using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
namespace CBP.DataAccess.Repository
{
    public class DogApplicationDetailRepository : Repository<DogApplicationDetail>, IDogApplicationDetailRepository
    {
        private ApplicationDbContext _db;
        public DogApplicationDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DogApplicationDetail obj)
        {
            _db.DogApplicationDetails.Update(obj);
        }
    }
}
