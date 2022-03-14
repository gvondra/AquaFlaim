using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public interface IExceptionService
    {
        Task Create(ISettings settings, params Models.Exception[] exceptions);
        Task Create(ISettings settings, params System.Exception[] exceptions);
    }
}
