using FirstApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class DummyController : Controller
    {
        private TaskInfoContext _ctx;

        public DummyController(TaskInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
