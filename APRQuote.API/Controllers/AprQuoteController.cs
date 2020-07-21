using APRQuote.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using APRQuote.Contracts;
using APRQuote.BLayer;

namespace APRQuote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprQuoteController : ControllerBase
    {
        private readonly Validator _validator;
        private readonly AprQuoteOps _aprQuoteOps;

        public AprQuoteController(IRepository<Vehicle> vehicleRepos,
                    IRepository<QuoteType> quoteTypeRepos,
                    IRepository<APRPercentRange> aprPercentRangeRepos,
                    IRepository<Quote> quoteRepos, IUoW aprUow)
        {
            _validator = new Validator();
            _aprQuoteOps = new AprQuoteOps(vehicleRepos, quoteTypeRepos, aprPercentRangeRepos, quoteRepos, aprUow);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var aprQuote = _aprQuoteOps.GetAllQuotes();
            return Ok(aprQuote);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            AprQuote aprQuote = _aprQuoteOps.GetQuotes(id);

            if (aprQuote == null)
            {
                return NotFound(id);
            }

            return Ok(aprQuote);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AprQuote aprQuote)
        {
            if (aprQuote == null || (!_validator.IsValid(aprQuote)))
            {
                return BadRequest("Invalid Quote");
            }

            if (_aprQuoteOps.AddQuote(aprQuote))
            {
                return Accepted(aprQuote);
            }
            else
            {
                return Conflict();
            }
        }
    }
}
