﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
        T[] GetAll();

    }
}
