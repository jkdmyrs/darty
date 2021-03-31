namespace Darty.Core.Commands.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Exceptions;
    using Darty.Core.Resources.Data;
    using Darty.Core.Settings;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class FetchGameCommand : IFetchGameCommand
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public FetchGameCommand(BlobStorageSettings blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings ?? throw new ArgumentNullException(nameof(blobStorageSettings));
        }
        public async Task<GameModelResource> Execute(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_blobStorageSettings.ConnecitonString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_blobStorageSettings.GameContainer);
            CloudBlockBlob blob = container.GetBlockBlobReference($"{id}.json");
            await blob.FetchAttributesAsync().ConfigureAwait(false);
            var data = new byte[blob.Properties.Length];
            await blob.DownloadToByteArrayAsync(data, 0).ConfigureAwait(false);

            if (data is null || data.Length == 0)
            {
                throw new GameNotFoundException(id);
            }

            return JsonSerializer.Deserialize<GameModelResource>(Encoding.UTF8.GetString(data));
        }
    }
}
