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
    internal class ProductRepository : Repository<Dog>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Dog obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Author = obj.Author;
                objFromDb.ProductImages = obj.ProductImages;
                //if(obj.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = obj.ImageUrl;
                //}
            }
        }
    }
}
