namespace CBP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IDogRepository Dog { get; }


        IGalleryImageRepository GalleryImage { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        IDogImageRepository DogImage { get; }
        IDogApplicationDetailRepository DogApplicationDetail { get; }

        void Save();
    }
}
