namespace Darty.Core.Commands.Impl
{
    using Azure.Storage.Blobs;
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Resources.Data;
    using Darty.Core.Settings;
    using System;
    using System.IO;
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
            BlobServiceClient serviceClient = new BlobServiceClient(_blobStorageSettings.ConnecitonString);
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(_blobStorageSettings.GameContainer);
            BlobClient blobClient = containerClient.GetBlobClient($"{id}.json");
            using (var downloadStream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(downloadStream).ConfigureAwait(false);
                downloadStream.Position = 0;

                using (var sr = new StreamReader(downloadStream))
                {
                    string gameJson = await sr.ReadToEndAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<GameModelResource>(gameJson);
                }
            }
        }
    }
}
