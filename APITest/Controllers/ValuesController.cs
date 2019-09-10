using APITest.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize]
        [Route("api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// post
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }
        /// <summary>
        /// post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }
        /// <summary>
        /// post
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
