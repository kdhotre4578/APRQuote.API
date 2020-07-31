using APRQuote.Core.Models;
using Microsoft.AspNetCore.Mvc;
using APRQuote.Core.Contracts;
using APRQuote.BLayer;

namespace APRQuote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprQuoteController : ControllerBase
    {
        private readonly Validator _validator;
        private readonly AprQuoteService _aprQuoteService;

        public AprQuoteController(IAprUoW aprUow)
        {
            _validator = new Validator();
            _aprQuoteService = new AprQuoteService(aprUow);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var aprQuote = _aprQuoteService.GetAllQuotes();
            return Ok(aprQuote);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            AprQuote aprQuote = _aprQuoteService.GetQuote(id);

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

            if (_aprQuoteService.AddQuote(aprQuote))
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
