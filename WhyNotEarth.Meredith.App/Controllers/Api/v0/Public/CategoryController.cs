using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhyNotEarth.Meredith.Data.Entity;

namespace WhyNotEarth.Meredith.App.Controllers.Api.v0.Public
{
    [ApiVersion("0")]
    [Route("api/v0/categories")]
    public class CategoryController : ControllerBase
    {
        private MeredithDbContext MeredithDbContext { get; }

        public CategoryController(MeredithDbContext meredithDbContext)
        {
            MeredithDbContext = meredithDbContext;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await MeredithDbContext.Categories.Select(c => new
            {
                c.Id,
                c.Name
            }).ToListAsync());
        }
    }
}