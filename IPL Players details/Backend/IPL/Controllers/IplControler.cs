using IPL.IPLDao;
using IPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPL.Controllers
{
    public class IplControler : Controller
    {
        private readonly IiplDao _iplDao;

        public IplControler(IiplDao IMatchDao)
        {
            _iplDao = IMatchDao;

        }

        [HttpPost("/q1", Name = "InsertPlayer")]
        public async Task<ActionResult<bool>> InsertPayment([FromBody] iplQ q)
        {
            if (q == null)
            {
                return BadRequest("Player detail not valid");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var value = await _iplDao.InsertPlayer(q);
                    if (value == 1)
                        return Ok(value);
                    else
                        return BadRequest("teamid not found");
                }
                return BadRequest();
            }
        }


        [Route("/q2")]
        [HttpGet]
        public async Task<ActionResult<List<Q2>>> Getq2()
        {
            var q = await _iplDao.Getq2();
            if (q == null)
            {
                return NotFound();
            }
            return Ok(q);
        }


        [Route("/q3")]
        [HttpGet]
        public async Task<ActionResult<List<Q3>>> Getq3()
        {
            var q = await _iplDao.Getq3();
            if (q == null)
            {
                return NotFound();
            }
            return Ok(q);
        }


        [Route("/q4")]
        [HttpGet]
        public async Task<ActionResult<List<Q4>>> Getq4(DateTime startDate, DateTime endDate)
        //public async Task<ActionResult<List<Q4>>> Getq4(string startDateString, string endDateString)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");
            var q = await _iplDao.Getq4(startDateString, endDateString);
            if (q == null)
            {
                return NotFound();
            }
            return Ok(q);
        }


    }
}
