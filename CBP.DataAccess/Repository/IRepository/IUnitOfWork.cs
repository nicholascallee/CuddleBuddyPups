namespace CBP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IDogRepository Dog { get; }

        IGalleryRepository Gallery { get; }
        IGalleryImageRepository GalleryImage { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        IDogImageRepository DogImage { get; }
        IApplicationDetailRepository ApplicationDetail { get; }

        IApplicationHeaderRepository ApplicationHeader { get; }

        void Save();
    }
}
