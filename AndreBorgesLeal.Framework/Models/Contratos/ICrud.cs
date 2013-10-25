﻿using AndreBorgesLeal.Framework.Models.Control;
using System;
using System.Collections.Generic;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface ICrud
    {
        Repository getObject(Object id);
        Validate Validate(Repository value, AndreBorgesLeal.Framework.Models.Enumeracoes.Crud operation);
        IEnumerable<Repository> ListAll();
        Repository Insert(Repository value);
        Repository Update(Repository value);
        Repository Delete(Repository value);
    }

    public interface ICrudContext<R> where R : Repository
    {
        R CreateRepository();
        R getObject(R id);
        Validate Validate(R value, AndreBorgesLeal.Framework.Models.Enumeracoes.Crud operation);
        IEnumerable<R> ListAll();
        R Insert(R value);
        R Update(R value);
        R Delete(R value);
    }

    public interface ICrudItemContext<R> : ICrudContext<R> where R : Repository
    {
        void SetListItem(IList<R> list);
        R SetKey(R r);
    }



}

