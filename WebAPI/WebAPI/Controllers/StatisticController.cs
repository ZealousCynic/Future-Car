using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class StatisticController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetLatestById(int id)
        {
            Statistic s;

            using(DAL.StatisticRepository repo = new DAL.StatisticRepository())
            {
                s = StatisticConverter.ConvertTo(repo.GetById(id));
            }

            if (s == null)
                return NotFound();
            return Ok(s);
        }
    }
}
