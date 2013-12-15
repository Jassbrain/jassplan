using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassTools
{
    public class JassCommonAttributesMapper<T1, T2, T3> : JassTools.IJassCommonAttributesMapper<T1,T2,T3>// where T2:T1 where T3:T1
    {
        public void map(T2 t2, T3 t3)
        {//assign the value of each property from t1 into t2
            var propsT1 = typeof(T1).GetProperties();
            foreach (var prop in propsT1)
            {
                //assign value of property prop from t1 into t2
                var property1Name = prop.Name;
                var property2 = typeof(T2).GetProperty(property1Name);
                var property3 = typeof(T3).GetProperty(property1Name);

                var property2Value = property2.GetValue(t2);
                property3.SetValue(t3,property2Value);
                //var property3Value = property3.GetValue(t3);
            }

        }

        public bool compare(T2 t2, T3 t3)
        {//assign the value of each property from t1 into t2

            var propsT1 = typeof(T1).GetProperties();

            foreach (var prop in propsT1)
            {
                //assign value of property prop from t1 into t2
                var property1Name = prop.Name;
                var property2 = typeof(T2).GetProperty(property1Name);
                var property3 = typeof(T3).GetProperty(property1Name);

                var property2Value = property2.GetValue(t2);
                var property3Value = property3.GetValue(t3);

                if (property2Value != null || property3Value != null)
                {

                    var property2ValueS = property2.GetValue(t2).ToString();
                    var property3ValueS = property3.GetValue(t3).ToString();

                    if (property2ValueS != property3ValueS) return false;
                }
                else
                {
                    if (property2Value != null || property3Value != null) return false;
                }
            }
            return true;
        }

    }
}
