using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassToolsTests
{


        public interface IProductRepository
        {
            List<IProduct> Select();
            IProduct Get(int id);
        }
    
       public interface IProduct
       {
           int Id {get; set;}
           string Name { get; set; }
       }
}