using System.Linq;
using ResponseMasking.AspNetCore.Filter.Faker;
using Microsoft.AspNetCore.Mvc;
using ResponseMasking.Filter;

namespace ResponseMasking.AspNetCore.Filter.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Generic List sample
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MaskResponseFilter]
        public IActionResult GetList()
        {
            var result = UserModelFaker.GetUsers();
            return Ok(result);
        }

        /// <summary>
        /// IQueryable sample
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        [MaskResponseFilter]
        public IActionResult GetListIQueryable()
        {
            var result = UserModelFaker.GetUsers().Where(q => q.Age > 2).AsQueryable();
            return Ok(result);
        }

        /// <summary>
        /// Complex type sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [MaskResponseFilter]
        public IActionResult Get(int id)
        {
            var result = UserModelFaker.GetUsers().FirstOrDefault(q => q.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Plain text sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/citizenshipnumber")]
        [MaskResponseFilter(3)]
        public IActionResult GetCitizenshipNumber(int id)
        {
            var result = UserModelFaker.GetUsers().FirstOrDefault(q => q.Id == id).CitizenshipNumber;
            return Ok(result);
        }
    }
}
