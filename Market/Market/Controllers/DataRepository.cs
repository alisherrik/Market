using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Controllers
{
    public class DataRepository:IDataRepository
    {
        private List<Products> products;
        private List<int> rassrochki;
        public List<Otchet> Otchets;
        public DataRepository()
        {
            rassrochki = new List<int> { 3, 6, 9, 12, 18, 24 };
            products = new List<Products>
            {

                 new Products
                {
                    id = 1,
                    Name = "Phone",
                    Price = 1000,
                    rassrochki = new Rassrochki{ rassrochki_ot =3,rassrochki_do =9},
                    procent=3
                },
                  new Products
                {
                    id = 2,
                    Name = "Laptop",
                    Price = 6000,
                    rassrochki = new Rassrochki{ rassrochki_ot =3,rassrochki_do =12},
                    procent=4
                    
                   
                },
                   new Products
                {
                    id = 3,
                    Name = "Tv",
                    Price = 3000,
                    rassrochki = new Rassrochki{ rassrochki_ot =3,rassrochki_do =18},
                    procent=5
                   
                },
            };
               
        }
      
        public List<Products> Getproducts()
        {
            return products;
        }

        List<Otchet> IDataRepository.Calculate(List<Operatsiya> operatsiyas)
        {
            Otchets = new List<Otchet>();
            foreach (var item in operatsiyas)
            {
                var _product = products.FirstOrDefault(s => s.id.Equals(item.idproduct));
                if (_product !=null)
                {
                    var index_limit_product = rassrochki.FindIndex(s => s.Equals(_product.rassrochki.rassrochki_do));
                    var index_limit_buying_product = rassrochki.FindIndex(s => s.Equals(item.howMonth));
                    var result = index_limit_buying_product - index_limit_product;
                    double procent_sum = 0;
                    if (result >0)
                    {
                        procent_sum = (_product.Price * _product.procent / 100)*result; 
                    }
                    var summa = (procent_sum + _product.Price) * item.CountProduct;
                    Otchets.Add(new Otchet { ProductName = _product.Name, Summa = summa, countProduct = item.CountProduct ,Month =item.howMonth});
                }
            }
            return Otchets;
        }

        List<int> IDataRepository.GetRassrochki()
        {
            return rassrochki;
        }

        Otchet IDataRepository.Otchet()
        {
            return Otchets.LastOrDefault();
        }
    }
}
