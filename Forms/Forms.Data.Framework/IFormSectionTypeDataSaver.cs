using AquaFlaim.Forms.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework
{
    public interface IFormSectionTypeDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, FormSectionTypeData formSectionTypeData);
        Task Update(ISqlTransactionHandler transactionHandler, FormSectionTypeData formSectionTypeData);
    }
}
