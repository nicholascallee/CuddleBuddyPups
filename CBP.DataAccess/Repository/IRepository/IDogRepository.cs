using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IDogRepository : IRepository<Dog>
    {
        void Update(Dog obj);
    }
}
