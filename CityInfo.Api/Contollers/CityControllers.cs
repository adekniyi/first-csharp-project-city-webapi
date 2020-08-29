using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Contollers
{
    [ApiController]
    [Route("api/cities")]
    public class CityControllers : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;


        public CityControllers(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public IActionResult GetCities()
        {
            var CitiesEntities = _cityInfoRepository.GetCities();

            //var result = new List<CityDtoWithOutPointOfInterest>();


            //foreach(var CityEntity in CitiesEntities)
            //{
            //    result.Add(new CityDtoWithOutPointOfInterest
            //    {
            //        id = CityEntity.id,
            //        Name = CityEntity.Name,
            //        Description = CityEntity.Description
            //    }
            //    );
            //}



            //return Ok(CityDataStore.Current.Cities);

            return Ok(_mapper.Map<IEnumerable<CityDtoWithOutPointOfInterest>>(CitiesEntities));
        }


        [HttpGet("{id}", Name ="CreateCity")]
        public IActionResult GetCity(int id , bool includePointsOfInterest = false)
        {

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if(city == null)
            {
                return NotFound();
            }

            if(includePointsOfInterest)
            {
                //var cityResult = new CityDTo()
                //{
                //    id = city.id,
                //    Name = city.Name,
                //    Description = city.Description
                //};

                //foreach(var poi in city.PointsOfInterest)
                //{
                //    cityResult.PointsOfInterest.Add(
                //        new PointOfInterestDto()
                //        {
                //            id = poi.id,
                //            Name = poi.Name,
                //            Description = poi.Description
                //        }
                //        );
                //}

                var cityResult = _mapper.Map<CityDTo>(city);


                return Ok(cityResult);

            }


            //var cityWithOutPointOfInterest = new CityDtoWithOutPointOfInterest()
            //{
            //    id = city.id,
            //    Name = city.Name,
            //    Description = city.Description
            //};
            return Ok(_mapper.Map<CityDtoWithOutPointOfInterest>(city));



            //return Ok(cityWithOutPointOfInterest);

            //var CityResult = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == id);

            //if(CityResult == null)
            //{
            //    return NotFound();
            //}

            //return Ok(CityResult);

            //return new JsonResult(CityDataStore.Current.Cities.FirstOrDefault(c => c.id == id));
        }

        [HttpPost]
        public IActionResult CreatePointOfIntrest(int id, CityForCreationDto city)
        {

            var cityResult = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == id);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CityDataStore.Current.Cities.Max(p => p.id);

            var finalPointOfInterest = new CityDTo()
            {
                id = ++maxPointOfInterestId,
                Name = city.Name,
                Description = city.Description
            };

            CityDataStore.Current.Cities.Add(finalPointOfInterest);

            return CreatedAtRoute("CreateCity",
                new {id = finalPointOfInterest.id }, finalPointOfInterest);

        }

    }
}
