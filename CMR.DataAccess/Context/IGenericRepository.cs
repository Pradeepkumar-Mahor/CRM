﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGMTApp.DataAccess.Context
{
    internal interface IGenericRepository<T>
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        bool Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}