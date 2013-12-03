using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassTools
{
    public class JassObjectMapper<T1, T2> //where T1 : new()
    {       
        public void mapProperties(T1 t1, T2 t2)
        {//assign the value of each property from t1 into t2
            var propsT1 = typeof(T1).GetProperties();
            var propsT2 = typeof(T2).GetProperties();
            foreach (var prop in propsT1)
            {
                //assign value of property prop from t1 into t2
                var property1Name = prop.Name;
                var property1Value = prop.GetValue(t1);
                var property2 = typeof(T2).GetProperty(property1Name);
                property2.SetValue(t2,property1Value);
            }
        }
    }
}
