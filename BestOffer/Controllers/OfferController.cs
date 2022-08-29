using BestDeal.Business.Offer.Contract;
using BestDeal.Model;
using Microsoft.AspNetCore.Mvc;

namespace BestDeal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferBusiness _offerBusiness;

        public OfferController(ILogger<OfferController> logger, IOfferBusiness offerBusiness)
        {
            _logger = logger;
            _offerBusiness = offerBusiness;
        }

        [HttpPost]
        [Route("CalculateBestOffer.{format}"), FormatFilter]
        public double Calculate([FromBody] List<Data> dataSet)
        {
            return _offerBusiness.CalculateBestOffer(dataSet);
        }
    }
}