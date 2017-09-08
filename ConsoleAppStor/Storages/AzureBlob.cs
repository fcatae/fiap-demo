using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleAppStor
{
    class AzureBlob : ITarget, ISource
    {
        private CloudBlobContainer _container;

        public AzureBlob(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            var client = storageAccount.CreateCloudBlobClient();

            var container = client.GetContainerReference("teste");

            container.CreateIfNotExists();

            this._container = container;
        }

        public void Add(string key, string value)
        {
            var blob = _container.GetBlockBlobReference(key);

            blob.UploadFromFile(value);
        }

        public string Get(string key)
        {
            var blob = _container.GetBlockBlobReference(key);

            var text = blob.DownloadText();

            return text;
        }
    }
}
