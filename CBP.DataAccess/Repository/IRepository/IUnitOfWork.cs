namespace CBP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IDogRepository Dog { get; }


        IShoppingCartRepository ShoppingCart { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        IDogImageRepository DogImage { get; }


        void Save();
    }
}
