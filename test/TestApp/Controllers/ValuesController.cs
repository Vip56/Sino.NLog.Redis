using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Newtonsoft.Json;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public class a
        {
            public string message { get; set; }
            public DateTime date { get; set; }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            log.Error(new ArgumentNullException("id"), JsonConvert.SerializeObject(new a
            {
                message = "看看能不能支持中文",
                date = DateTime.Now
            }));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
