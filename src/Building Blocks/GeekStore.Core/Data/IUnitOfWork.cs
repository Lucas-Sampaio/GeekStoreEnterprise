using System.Threading.Tasks;

namespace GeekStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
