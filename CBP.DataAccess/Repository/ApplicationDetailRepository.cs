using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
namespace CBP.DataAccess.Repository
{
    public class ApplicationDetailRepository : Repository<ApplicationDetail>, IApplicationDetailRepository
    {
        private ApplicationDbContext _db;
        public ApplicationDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationDetail obj)
        {
            _db.ApplicationDetails.Update(obj);
        }
    }
}
