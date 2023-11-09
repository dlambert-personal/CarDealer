using DealerService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DealerService.Controllers
{
    [ApiController]
    [Route("/dealer/{dealerId:int}/brandaffiliation")]
    public class DealerBrandController : Controller
    {
        private List<Dealer> _sampleData = new List<Dealer>();
        public DealerBrandController()
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
        //public IActionResult Index()
        //{
        //    return View();
        //}


        //...

        [HttpGet]
        [Route("{brandaffiliationId:int}")] // Matches GET /dealer/123/brandaffiliation/456
        public IActionResult GetBrandAffiliation([FromRoute] int dealerId, [FromRoute] int brandaffiliationId)
        {
            //... validate campus id along with building id 
            Dealer? dealer = _sampleData.SingleOrDefault(d => d.Id == dealerId);
            if (dealer == null)
            {
                return NotFound();
            }
            Brand? brand = dealer.BrandAffiliation.SingleOrDefault(b => b.Id == brandaffiliationId);

            return brand == null ? NotFound() : Ok(brand);
        }
    }
}
