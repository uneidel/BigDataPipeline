using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Frontendui.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //TODO: create something like a configurationmanager
            var lines = System.IO.File.ReadLines(@"C:\local\Lv3\Data\ml-10M100K\movies.dat").Skip(id).Take(100).ToList(); //Lazy loading
            var data =  JsonConvert.SerializeObject(lines);
            return data;
        }

       
    }
}
