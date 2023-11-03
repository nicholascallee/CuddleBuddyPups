using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
namespace CBP.DataAccess.Repository
{
    public class ApplicationHeaderRepository : Repository<ApplicationHeader>, IApplicationHeaderRepository
    {
        private ApplicationDbContext _db;
        public ApplicationHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationHeader obj)
        {
            _db.ApplicationHeaders.Update(obj);
        }


        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var applicationFromDb = _db.ApplicationHeaders.FirstOrDefault(u => u.Id == id);
            if (applicationFromDb != null)
            {
                applicationFromDb.ApplicationStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    applicationFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
