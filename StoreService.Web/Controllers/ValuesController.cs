using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StoreService.Web.Models;
using IDatabase = StackExchange.Redis.IDatabase;

namespace StoreService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        StoreContext context;
        private ILogger<ValuesController> logger;
        IConfiguration config;

        public ValuesController(StoreContext context, 
                                ILogger<ValuesController> logger,
                                IConfiguration config)
        {
            this.context = context;
            this.logger = logger;
            this.config = config;
        }

        [HttpGet("Cache")]
        public ActionResult<List<string>> GetCache()
        {
            var strings = new List<string>();
            var lazyConn = new Lazy<ConnectionMultiplexer>(() => {
                string cacheConnection = config["CacheConnection"];
                return ConnectionMultiplexer.Connect(cacheConnection);
            });

            IDatabase cache = lazyConn.Value.GetDatabase();
            strings.Add(cache.Execute("PING").ToString());
            strings.Add(cache.StringSet("Message","Hello!").ToString());
            strings.Add(cache.StringGet("Message").ToString());
            lazyConn.Value.Dispose();

            return strings;
        }

        /// <summary>
        /// Returns Orders
        /// </summary>
        /// <remarks>
        /// GET /api/Values/Get
        /// </remarks>
        [HttpGet]
        public ActionResult<IEnumerable<Orders>> Get()
        {
            logger.LogInformation("Executing Get Action...");
            var orders = context.Orders
                    .Include(o => o.OrderItems)
                    .Select(o => o).ToList();
            return orders;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
