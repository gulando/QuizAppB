using System;
using System.Collections.Generic;
using System.Linq;


namespace QuizData.Repository
{
    interface IRepository<T> : IDisposable  where T : class
    {
        T GetObjByID(int objID);
        
        IEnumerable<T> GetObjList();
        
        T AddObj(T obj);
        
        T UpdateObj(T obj);
        
        T DeleteObj(int objID);
    }
}