using AquaFlaim.Forms.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework
{
    public interface IFormQuestionTypeDataFactory
    {
        Task<IEnumerable<FormQuestionTypeData>> GetByFormSectionTypeId(ISqlSettings settings, int formSectionTypeId);
        Task<IEnumerable<FormQuestionTypeData>> GetByFormTypeId(ISqlSettings settings, int formTypeId);
    }
}
