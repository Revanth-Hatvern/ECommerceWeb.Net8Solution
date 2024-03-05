﻿using System;
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
            this.dbset=_db.Set<T>();
            //   _db.Categories == dbset;    //Understannding  _db.Categories.Add() === dbset.Add()
        }
        public void Add(T entity)
        {
         dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbset;
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
