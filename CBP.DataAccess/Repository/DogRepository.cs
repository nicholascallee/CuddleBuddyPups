using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBP.DataAccess.Repository
{
    internal class DogRepository : Repository<Dog>, IDogRepository
    {
        private ApplicationDbContext _db;
        public DogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Dog obj)
        {
            _db.Dogs.Update(obj);
        }
    }
}
