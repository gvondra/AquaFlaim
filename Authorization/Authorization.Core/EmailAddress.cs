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
    public class EmailAddress : IEmailAddress
    {
        private readonly EmailAddressData _data;
        private readonly IEmailAddressDataSaver _dataSaver;

        public EmailAddress(EmailAddressData data,
            IEmailAddressDataSaver dataSaver)
        {
            _data = data;
            _dataSaver = dataSaver;
        }

        public Guid EmailAddressId { get => _data.EmailAddressId; }
        public string Address { get => _data.Address; }
        public DateTime CreateTimestamp { get => _data.CreateTimestamp; }

        public Task Create(ITransactionHandler transactionHandler) => _dataSaver.Create(transactionHandler, _data);
    }
}
