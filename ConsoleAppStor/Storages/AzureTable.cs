using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleAppStor
{
    class AzureTable : ITarget, ISource
    {
        class AzureTableEntity : TableEntity
        {
            public string Titulo { get; set; }
            public string Descricao { get; set; }
        }

        private CloudTable _table;

        public AzureTable(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            var client = storageAccount.CreateCloudTableClient();

            this._table = client.GetTableReference("tbExemplo");

            this._table.CreateIfNotExists();
        }

        public void Add(string key, string value)
        {
            var entity = new AzureTableEntity() {
                PartitionKey = key,
                RowKey = key,
                Titulo = value, Descricao = "Adicionando campo"
            };

            TableOperation insert = TableOperation.InsertOrReplace(entity);

            _table.Execute(insert);
        }

        public string Get(string key)
        {
            TableOperation retrieve = TableOperation.Retrieve<AzureTableEntity>(key, key);

            var result = _table.Execute(retrieve);

            return ((AzureTableEntity)result.Result).Titulo;
        }
    }
}
