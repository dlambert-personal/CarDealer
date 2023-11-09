using DealerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web.Resource;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DealerService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class DealerController : ControllerBase
    {
        private List<Dealer> _sampleData = new List<Dealer>();

        public DealerController() 
        {
            string json = System.IO.File.ReadAllText("./Data/sampleDealers.json");
            _sampleData = JsonSerializer.Deserialize<List<Dealer>>(json);
            Dealer cap = new Dealer();
            {
                cap.Id = 3;
                cap.Name = "Capital City Acura";
                cap.Description = "desc";
                cap.BrandAffiliation.Add(Brand.Acura);
            }
            _sampleData.Add(cap);
            string saveme = JsonSerializer.Serialize(_sampleData);
        }



        //public IActionResult<IQueryable<Customer>> Get() { /*... */}

        //public IActionResult<Customer> Get(int key) { /* ... */ }

        //public IActionResult<IQueryable<Order>> GetOrders(int key) {/* ... */}

        //[EnableNestedPaths]


        // GET: DealerController
        [HttpGet(Name = "GeDealer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Dealer>))]
        public IActionResult Get()
        {
            return Ok(_sampleData.ToArray());
        }

        // GET: DealerController/Details/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dealer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Details(int id)
        {
            Dealer? dealer = _sampleData.SingleOrDefault(d => d.Id == id);
            return dealer == null ? NotFound() : Ok(dealer);
        }


        //POST: create new dealer
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Dealer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateDealer(Dealer dealer)
        {
            Guard.Against
            if (dealer.Id > 0) // validation failed
            {
                return BadRequest("Do not supply ID for new entity.");
            }
            var dvalidator = new DealerValidator();
            var result = dvalidator.Validate(dealer);
            if (result.IsValid)
            {
                _sampleData.Add(dealer);  // doesn't really pay until we pull the data structure out of here
                                          //SaveChangesAsync();

                return CreatedAtAction(nameof(CreateDealer), new { id = dealer.Id }, dealer);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //PUT: edit existing dealer
        [HttpPut("{id}")]
        public ActionResult<Dealer> EditDealer(int id, Dealer dealer)
        {
            if (dealer.Id == 0 || id != dealer.Id) // validation failed
            {
                return BadRequest("Please supply ID for edit.");
            }
            Dealer? founddealer = _sampleData.SingleOrDefault(d => d.Id == dealer.Id);
            if (founddealer == null)
                return NotFound();

            founddealer = dealer;
            //SaveChangesAsync();

            return founddealer;
        }


        // DELETE: Delete an existing Dealer
        [HttpDelete("{id}")]
        public ActionResult DeleteDealer(int id)
        {
            if (id == 0) // validation failed
            {
                return BadRequest("Please supply ID for delete.");
            }
            Dealer? founddealer = _sampleData.SingleOrDefault(d => d.Id == id);
            if (founddealer == null)
                return NotFound();

            _sampleData.Remove(founddealer);
            return Ok();
        }




    }
}
