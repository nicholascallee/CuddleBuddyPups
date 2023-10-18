using CBP.Models;


namespace CBP.DataAccess.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<DogImage>
    {
        void Update(DogImage obj);
    }
}
