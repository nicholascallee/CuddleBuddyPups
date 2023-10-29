using CBP.Models;

namespace CBP.DataAccess.Repository.IRepository
{
    public interface IDogApplicationDetailRepository : IRepository<DogApplicationDetail>
    {
        void Update(DogApplicationDetail obj);
    }
}
