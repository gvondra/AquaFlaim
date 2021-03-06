using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.CommonCore
{
    public class TransactionHandler : ITransactionHandler
    {
        private ISettings _settings;

        public TransactionHandler(ISettings settings)
        {
            _settings = settings;
        }

        public DbConnection Connection { get; set; }
        public BrassLoon.DataClient.IDbTransaction Transaction { get; set; }

        public Func<Task<string>> GetAccessToken => _settings.GetDatabaseAccessToken();

        public Task<string> GetConnectionString() => _settings.GetConnetionString();
    }
}
