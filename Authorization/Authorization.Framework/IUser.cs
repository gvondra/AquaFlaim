using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IUser
    {
        Guid UserId { get; }
        string ReferenceId { get; }
        string Name { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }

        Task<IEmailAddress> GetEmailAddress(ISettings settings);
        void SetEmailAddress(IEmailAddress emailAddress);
        Task Create(ITransactionHandler transactionHandler);
        Task Update(ITransactionHandler transactionHandler);
    }
}
