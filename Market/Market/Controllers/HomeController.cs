using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Market.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IDataRepository _repository;
        public HomeController(IDataRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public List<Otchet> GetData(List<Operatsiya> operatsiya) => _repository.Calculate(operatsiya);

        [HttpGet]
        public List<Products> Products() => _repository.Getproducts();

        [HttpGet]
        public List<int> Rassrochki() => _repository.GetRassrochki();
        [HttpGet]
        public Otchet otchet() => _repository.Otchet();
    }
}
