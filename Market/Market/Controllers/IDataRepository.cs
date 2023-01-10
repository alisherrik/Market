using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Controllers
{
   public interface IDataRepository
    {
        public List<Products> Getproducts();
        public List<int> GetRassrochki();
        public List<Otchet> Calculate( List<Operatsiya> operatsiyas);
        public Otchet Otchet();
    }
}
