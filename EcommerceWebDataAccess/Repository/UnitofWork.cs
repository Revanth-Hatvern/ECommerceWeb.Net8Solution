using EcommerceWebDataAccess.Repository.IRepository;
using ECommerceWebDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebDataAccess.Repository
{
    public class UnitofWork : IUnitOfWork
    {
        public ICategoryRepository category {  get;private  set; }

        private readonly ApplicationDbContext _db;
        public UnitofWork(ApplicationDbContext db) 
        {
            _db = db;
            category=new CategoryRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
