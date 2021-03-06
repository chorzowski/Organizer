﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjektTest2.Models
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}