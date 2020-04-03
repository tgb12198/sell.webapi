using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Common.Comm
{
    public class CopyHelper
    {
        public static List<TEntity> Clone<TEntity>(List<TEntity> originList,List<TEntity> currentList) where TEntity : class, new()
        {
            try
            {
                Type sourceType = typeof(TEntity);
                foreach (var o1 in originList)
                {
                    TEntity curr = new TEntity();
                    foreach (PropertyInfo propInfo in (sourceType.GetProperties()))
                    {
                        var val = propInfo.GetValue(o1, null);
                        propInfo.SetValue(curr, val);
                    }
                    currentList.Add(curr);
                }
                return currentList;
            }
            catch
            {
                return currentList;
            }
        }

    }
}
