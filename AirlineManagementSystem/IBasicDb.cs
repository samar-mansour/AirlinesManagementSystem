using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    //Generic Interface that allows every class/interface to use it/inherited. 
    //"Base database interface for all DAO classes".
    interface IBasicDb<T> where T : IPoco
    {
        T Get(int id);
        IList<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);

    }
}
