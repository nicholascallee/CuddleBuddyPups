using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;

namespace CBP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IDogRepository Dog { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IDogImageRepository DogImage { get; private set; }
        public IApplicationDetailRepository ApplicationDetail { get; private set; }

        public IApplicationHeaderRepository ApplicationHeader { get; private set; }

        public IGalleryImageRepository GalleryImage { get; private set; }
        public IGalleryRepository Gallery {  get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            GalleryImage = new GalleryImageRepository(_db);
            Gallery = new GalleryRepository(_db);
            Dog = new DogRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            DogImage = new DogImageRepository(_db);
            ApplicationDetail = new ApplicationDetailRepository(_db);
            ApplicationHeader = new ApplicationHeaderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
