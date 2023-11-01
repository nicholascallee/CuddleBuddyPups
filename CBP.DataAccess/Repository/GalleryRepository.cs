using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;

namespace CBP.DataAccess.Repository
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        private ApplicationDbContext _db;
        public GalleryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Gallery obj)
        {
            _db.Gallerys.Update(obj);
        }
    }
}
