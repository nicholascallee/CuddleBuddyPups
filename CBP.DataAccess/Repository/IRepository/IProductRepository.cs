using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Dog>
    {
        void Update(Dog obj);
    }
}
