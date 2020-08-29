using CityInfo.Api.Context;
using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public City GetCity(int cityId ,bool includePOintsOfInterest)
        {
            if(includePOintsOfInterest)
            {
                return _context.Cities.Include( c => c.PointsOfInterest)
                    .Where(c => c.id == cityId).FirstOrDefault();
            }
            return _context.Cities.Where(c => c.id == cityId).FirstOrDefault();
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.Include(c => c.PointsOfInterest).ToList();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.pointsOfInterest.Where(p => p.cityId == cityId && p.id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterests(int cityId)
        {
            return _context.pointsOfInterest.Where(p => p.cityId == cityId).ToList();
        }

        public bool cityExist(int cityid)
        {
            return _context.Cities.Any(c => c.id == cityid);
        }

        public void AddPointOfInterestForCity(int cityid, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityid, false);

            city.PointsOfInterest.Add(pointOfInterest);
        }

        public void UpdatePointOfInterest(int cityid, PointOfInterest pointOfInterest)
        {

        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
           _context.Remove(pointOfInterest);
        }


        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
