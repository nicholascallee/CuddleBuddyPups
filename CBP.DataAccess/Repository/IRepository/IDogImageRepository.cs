using CBP.Models;


namespace CBP.DataAccess.Repository.IRepository
{
    public interface IDogImageRepository : IRepository<DogImage>
    {
        void Update(DogImage obj);
    }
}
