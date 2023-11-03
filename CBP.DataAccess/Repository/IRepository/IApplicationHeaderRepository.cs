using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IApplicationHeaderRepository : IRepository<ApplicationHeader>
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        void Update(ApplicationHeader obj);
    }
}
