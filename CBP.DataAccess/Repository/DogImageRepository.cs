using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CBP.DataAccess.Repository
{
    public class DogImageRepository : Repository<DogImage>, IDogImageRepository
    {
        private ApplicationDbContext _db;
        public DogImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DogImage obj)
        {
            _db.DogImages.Update(obj);
        }
    }
}
