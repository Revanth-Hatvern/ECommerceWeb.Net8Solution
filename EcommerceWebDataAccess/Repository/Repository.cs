using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EcommerceWebDataAccess.Repository.IRepository;
using ECommerceWebDataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebDataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset=_db.Set<T>();     //   _db.Categories == dbset;    //Understannding  _db.Categories.Add() === dbset.Add()
            _db.Products.Include(u => u.Category).Include(u=>u.CategoryId);

        }
        public void Add(T entity)
        {
         dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var incluedProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluedProp);
                }
            }
            return query.FirstOrDefault();
        }

        //Category,CoverType
        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = dbset;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var incluedProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(incluedProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(T entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
