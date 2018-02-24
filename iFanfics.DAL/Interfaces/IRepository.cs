﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iFanfics.DAL.Interfaces {
    public interface IRepository<TEntity, TKey> where TEntity: class {
        IQueryable<TEntity> Query();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        Task<TEntity> GetById(TKey id);

        Task<TEntity> Create(TEntity item);

        Task<TEntity> Update(TEntity item);

        Task<TEntity> Delete(TKey id);

        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
