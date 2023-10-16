﻿using CBP.DataAccess.Data;
using CBP.DataAccess.Repository.IRepository;

namespace CBP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IProductRepository Product { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IProductImageRepository ProductImage { get; private set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCart = new ShoppingCartRepository(_db);
            Product = new ProductRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ProductImage = new ProductImageRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
