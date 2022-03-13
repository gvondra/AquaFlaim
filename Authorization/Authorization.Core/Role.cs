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
    public class Role : IRole
    {
        private readonly RoleData _data;
        private readonly IRoleDataSaver _dataSaver;

        public Role(RoleData data,
            IRoleDataSaver dataSaver)
        {
            _data = data;
            _dataSaver = dataSaver;
        }

        public int RoleId => _data.RoleId;

        public string Name { get => _data.Name; set => _data.Name = (value ?? string.Empty).Trim(); }

        public string PolicyName => _data.PolicyName;

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public Task Create(ITransactionHandler transactionHandler) => _dataSaver.Create(transactionHandler, _data);

        public Task Update(ITransactionHandler transactionHandler) => _dataSaver.Update(transactionHandler, _data);
    }
}
