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
    public class User : IUser
    {
        private readonly UserData _data;
        private readonly IUserDataSaver _dataSaver;
        private readonly IEmailAddressFactory _emailFactory;
        private IEmailAddress _email;

        public User(UserData data,
            IUserDataSaver dataSaver,
            IEmailAddressFactory emailAddressFactory)
        {
            _data = data;
            _dataSaver = dataSaver;
            _emailFactory = emailAddressFactory;
        }

        public Guid UserId => _data.UserId;
        public string ReferenceId => _data.ReferenceId;
        internal Guid EmailAddressId { get => _data.EmailAddressId; set => _data.EmailAddressId = value; }
        public string Name { get => _data.Name; set => _data.Name = value; }
        public DateTime CreateTimestamp => _data.CreateTimestamp;
        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public async Task Create(ITransactionHandler transactionHandler)
        {
            if (_email != null)
                EmailAddressId = _email.EmailAddressId;
            await _dataSaver.Create(transactionHandler, _data);
        }

        public async Task<IEmailAddress> GetEmailAddress(ISettings settings)
        {
            if (_email == null && !EmailAddressId.Equals(Guid.Empty))
                _email = await _emailFactory.Get(settings, EmailAddressId);
            return _email;
        }

        public void SetEmailAddress(IEmailAddress emailAddress)
        {
            if (emailAddress == null)
                throw new ArgumentNullException(nameof(emailAddress));
            _email = emailAddress;
        }

        public async Task Update(ITransactionHandler transactionHandler)
        {
            if (_email != null)
                EmailAddressId = _email.EmailAddressId;
            await _dataSaver.Update(transactionHandler, _data);
        }
    }
}
