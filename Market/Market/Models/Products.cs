using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Products
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Rassrochki rassrochki { get; set; } = new Rassrochki();
        public double procent { get; set; }

    }
}
