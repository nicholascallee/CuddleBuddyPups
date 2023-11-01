using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;

namespace CBP.DataAccess.Repository
{
    public class GalleryImageRepository : Repository<GalleryImage>, IGalleryImageRepository
    {
        private ApplicationDbContext _db;
        public GalleryImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GalleryImage obj)
        {
            _db.GalleryImages.Update(obj);
        }
    }
}
