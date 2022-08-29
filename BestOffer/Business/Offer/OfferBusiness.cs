using BestDeal.Business.Offer.Contract;
using BestDeal.Model;
using GeoCoordinatePortable;

namespace BestDeal.Business.Offer
{
    public class OfferBusiness : IOfferBusiness
    {
        /// <summary>
        /// Default conversion factor used by the transportation industry to calculate volumetric weight
        /// </summary>
        private const int CONVERSION_FACTOR = 400;

        /// <summary>
        /// This cost is a default value and is used as my fixed cost
        /// This cost is based on COP(Colombian Peso)
        /// </summary>
        private const double FIXED_COST = 15000;

        /// <summary>
        /// This cost is a default value and is used as my variable cost, for example this cost was calculate based on the gasoline, maintaing, operator and tires
        /// This cost is based on COP (Colombian Peso)
        /// </summary>
        private const double VARIABLE_COST = 12000;

        public double CalculateBestOffer(List<Data> dataSet)
        {
            if (dataSet.Any(data => CalculateVolume(data.Dimensions!) * CONVERSION_FACTOR < 5))
            {
                return FIXED_COST + VARIABLE_COST;
            }

            //Offer = FC + VC(D)
            return dataSet.Select(data => FIXED_COST + (VARIABLE_COST * (CalculateDistances(data.SourceAddress!, data.DestinationAddress!) / 1000))).OrderBy(offer => offer).FirstOrDefault();
        }

        public double CalculateDistances(string source, string destination)
        {
            var sourceCoord = new GeoCoordinate(double.Parse(source.Split(",")[0]), double.Parse(source.Split(",")[1]));
            var destinationCoord = new GeoCoordinate(double.Parse(destination.Split(",")[0]), double.Parse(destination.Split(",")[1]));

            return sourceCoord.GetDistanceTo(destinationCoord);
        }

        public double CalculateVolume(double[] sizes)
        {
            double result = 1;

            foreach (double value in sizes)
            {
                result *= value;
            }

            return result;
        }
    }
}
