using APRQuote.Core.Models;
using Microsoft.AspNetCore.Mvc;
using APRQuote.Core.Contracts;
using APRQuote.BLayer;
using System.Threading.Tasks;
using APRQuote.API.Validators;

namespace APRQuote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprQuoteController : ControllerBase
    {
        private readonly AprQuoteService _aprQuoteService;

        public AprQuoteController(IAprUoW aprUow)
        {
            _aprQuoteService = new AprQuoteService(aprUow);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
                var aprQuote = await _aprQuoteService.GetAllQuotes();
                return Ok(aprQuote);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            AprQuote aprQuote = await _aprQuoteService.GetQuote(id);

            if (aprQuote == null)
            {
                return NotFound(id);
            }

            return Ok(aprQuote);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AprQuote aprQuote)
        {
            var validator = new QuoteValidator();
            var validationResult = await validator.ValidateAsync(aprQuote);

            if(!validationResult.IsValid)
            {
                return BadRequest("Invalid Quote : " + validator.GetErrorDetails(validationResult));
            }

            var result = await _aprQuoteService.AddQuote(aprQuote);

            if (result)
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
