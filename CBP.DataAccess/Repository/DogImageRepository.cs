using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;

namespace CBP.DataAccess.Repository
{
    public class DogImageRepository : Repository<DogImage>, IDogImageRepository
    {
        private ApplicationDbContext _db;
        public DogImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DogImage obj)
        {
            _db.DogImages.Update(obj);
        }
    }
}
