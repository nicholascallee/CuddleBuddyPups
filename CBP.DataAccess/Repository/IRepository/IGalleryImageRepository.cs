using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IGalleryImageRepository : IRepository<GalleryImage>
    {
        void Update(GalleryImage obj);
    }
}
