using AquaFlaim.Authorization.Data.Framework;
using AquaFlaim.Authorization.Data.Framework.Models;
using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Core
{
    public class Client : IClient
    {
        private readonly ClientData _data;
        private readonly IClientDataFactory _dataFactory;
        private readonly IClientDataSaver _dataSaver;
        private string _newSecret;

        public Client(ClientData data,
            IClientDataFactory dataFactory,
            IClientDataSaver dataSaver)
        {
            _data = data;
            _dataFactory = dataFactory;
            _dataSaver = dataSaver;
        }

        public Guid ClientId => _data.ClientId;

        public string Name { get => _data.Name; set => _data.Name = value; }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public async Task Create(ITransactionHandler transactionHandler)
        {
            await _dataSaver.Create(transactionHandler, _data);
            await SaveNewSecret(transactionHandler);
        }

        public void SetSecret(string secret)
        {
            _newSecret = secret;
        }

        public async Task Update(ITransactionHandler transactionHandler)
        {
            await _dataSaver.Update(transactionHandler, _data);
            await SaveNewSecret(transactionHandler);
        }

        public async Task<bool> VerifySecret(ISettings settings, string secret)
        {
            return ClientSecretProcessor.Verify(secret,
                await GetSecretHash(settings)
                );
        }

        private async Task<byte[]> GetSecretHash(ISettings settings)
        {
            return (await _dataFactory.GetClientCredentials(new DataSettings(settings), ClientId))
                .OrderByDescending(c => c.CreateTimestamp)
                .Select<ClientCredentialData, byte[]>(c => c.Secret)
                .FirstOrDefault();              
        }

        private async Task SaveNewSecret(ITransactionHandler transactionHandler)
        {
            if (!string.IsNullOrEmpty(_newSecret))
            {
                ClientCredentialData data = new ClientCredentialData
                { 
                    ClientId = ClientId,
                    Secret = ClientSecretProcessor.Hash(_newSecret)
                };
                await _dataSaver.Create(transactionHandler, data);
            }            
        }
    }
}
