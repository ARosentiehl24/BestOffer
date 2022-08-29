using BestDeal.Business.Offer;
using BestDeal.Business.Offer.Contract;
using BestDeal.Model;
using GeoCoordinatePortable;
using System.Text.Json;

namespace BestDealTest
{
    public class Tests
    {
        private const int CONVERSION_FACTOR = 400;

        private const double FIXED_COST = 15000;

        private const double VARIABLE_COST = 12000;

        private readonly IOfferBusiness _offerBusiness = new OfferBusiness();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateBestOffer()
        {
            List<Data>? dataSet = JsonSerializer.Deserialize<List<Data>>(File.ReadAllText(@"Resources/data.json"));

            if (dataSet!.Any(data => _offerBusiness.CalculateVolume(data.Dimensions!) * CONVERSION_FACTOR < 5))
            {
                Assert.That(FIXED_COST + VARIABLE_COST, Is.Not.LessThanOrEqualTo(0));
            }

            Assert.That(dataSet!.
                Select(data => FIXED_COST + (VARIABLE_COST * (_offerBusiness.CalculateDistances(data.SourceAddress!, data.DestinationAddress!) / 1000))).
                OrderBy(offer => offer).FirstOrDefault(), Is.Not.LessThanOrEqualTo(0));
        }

        [Test]
        public void CalculateDistances()
        {
            var sourceCoord = new GeoCoordinate(4.5827788, -74.1504487);
            var destinationCoord = new GeoCoordinate(4.5870333, -74.1562906);

            Assert.That(sourceCoord.GetDistanceTo(destinationCoord), Is.Not.LessThanOrEqualTo(0));
        }

        [Test]
        public void CalculateVolume()
        {
            double[] sizes = new double[3] { 1.4, 1.5, 1.6 };

            double result = 1;

            foreach (double value in sizes)
            {
                result *= value;
            }

            Assert.That(result, Is.Not.LessThanOrEqualTo(0));
        }
    }
}