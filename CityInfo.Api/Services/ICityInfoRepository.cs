using CityInfo.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePOintsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterests(int cityId);
        PointOfInterest GetPointOfInterest(int cityId, int pointOfInterestId);


        bool cityExist(int cityid);

        void AddPointOfInterestForCity(int cityid, PointOfInterest pointOfInterest);

        bool save();

        void UpdatePointOfInterest(int cityid, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
