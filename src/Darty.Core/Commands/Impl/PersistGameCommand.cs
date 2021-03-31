namespace Darty.Core.Commands.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Resources.Data;
    using Darty.Core.Settings;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class PersistGameCommand : IPersistGameCommand
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public PersistGameCommand(BlobStorageSettings blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings ?? throw new ArgumentNullException(nameof(blobStorageSettings));
        }

        public async Task Execute(GameModelResource game)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_blobStorageSettings.ConnecitonString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_blobStorageSettings.GameContainer);
            CloudBlockBlob blob = container.GetBlockBlobReference($"{game.Id}.json");
            blob.Properties.ContentType = "application/json";

            using (Stream uploadStream = new MemoryStream())
            {
                void LoadStreamWithJson(Stream streamToLoad)
                {
                    StreamWriter writer = new StreamWriter(streamToLoad);
                    writer.Write(JsonSerializer.Serialize(game));
                    writer.Flush();
                    streamToLoad.Position = 0;
                }
                LoadStreamWithJson(uploadStream);
                await blob.UploadFromStreamAsync(uploadStream).ConfigureAwait(false);
            }
        }
    }
}
