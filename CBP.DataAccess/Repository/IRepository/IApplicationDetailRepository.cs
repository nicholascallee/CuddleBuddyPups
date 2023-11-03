using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IApplicationDetailRepository : IRepository<ApplicationDetail>
    {
        void Update(ApplicationDetail obj);
    }
}
