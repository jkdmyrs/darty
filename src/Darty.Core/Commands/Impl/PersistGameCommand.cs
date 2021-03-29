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

    public class PersistGameCommand : IPersistGameCommand
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public PersistGameCommand(BlobStorageSettings blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings ?? throw new ArgumentNullException(nameof(blobStorageSettings));
        }

        public async Task Execute(GameModelResource game)
        {
            BlobServiceClient serviceClient = new BlobServiceClient(_blobStorageSettings.ConnecitonString);
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(_blobStorageSettings.GameContainer);
            BlobClient blobClient = containerClient.GetBlobClient($"{game.Id}.json");
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
                await blobClient.UploadAsync(uploadStream).ConfigureAwait(false);
            }
        }
    }
}
