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
    public class ProductImageRepository : Repository<DogImage>, IProductImageRepository
    {
        private ApplicationDbContext _db;
        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DogImage obj)
        {
            _db.ProductImages.Update(obj);
        }
    }
}
