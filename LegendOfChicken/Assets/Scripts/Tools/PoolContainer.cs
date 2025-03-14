using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools.Pool
{
    public class PoolContainer<T>
    {
        public List<T> Objs = new List<T>();

        public bool TryGetObject(out T obj, Func<T, bool> func)
        {
            obj = Objs.FirstOrDefault(func);
            if (obj is T)
                return true;
            return false;
        }
    }
}
