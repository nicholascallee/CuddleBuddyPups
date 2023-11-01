using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        void Update(Gallery obj);
    }
}
