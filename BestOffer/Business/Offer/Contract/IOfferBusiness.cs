using BestDeal.Model;

namespace BestDeal.Business.Offer.Contract
{
    public interface IOfferBusiness
    {
        double CalculateBestOffer(List<Data> dataSet);

        double CalculateVolume(double[] sizes);

        double CalculateDistances(string source, string destination);
    }
}
