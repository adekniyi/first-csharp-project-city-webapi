using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CityInfo.Api.Contollers
{
    [ApiController]
    [Route("api/cities/{cityid}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController>logger,
            IMailService mailService, ICityInfoRepository cityInfoRepository, IMapper mapper
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPointOfInterest(int cityid)
        {



            try
            {
               // throw new Exception("new exception");
            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityid);

            if(!_cityInfoRepository.cityExist(cityid))
            {
                _logger.LogInformation($"City with id {cityid} wasn't foumd when accessing points of interest");
                return NotFound();
            }

                var pointsOfIntereForCity = _cityInfoRepository.GetPointsOfInterests(cityid);


                //var pointOfInterestForCityResult = new List <PointOfInterestDto>();

                //foreach(var poi in pointsOfIntereForCity)
                //{
                //    pointOfInterestForCityResult.Add(
                //        new PointOfInterestDto()
                //        {
                //            id = poi.id,
                //            Name = poi.Name,
                //            Description = poi.Description
                //        });
                //}

                //return Ok(pointOfInterestForCityResult);
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfIntereForCity));

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting point of interest for the id {cityid}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }

        }


        [HttpGet("{id}", Name ="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityid , int id)
        {
            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityid);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(city => city.id == id);

            //    if(pointOfInterest == null)
            //{
            //    return NotFound();
            //}


            //return Ok(pointOfInterest);

            if (!_cityInfoRepository.cityExist(cityid))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterest(cityid, id);

                if(pointOfInterest == null)
                {
                    return NotFound();
                }

            //var pointOfInterestResult = new PointOfInterestDto()
            //{
            //    id = pointOfInterest.id,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //return Ok(pointOfInterestResult);

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }

        [HttpPost]
        public IActionResult CreatePointOfIntrest(int cityid, PointOfInterestForCreationDto pointOfInterst)
        {

            if(pointOfInterst.Description == pointOfInterst.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided Description should be different from the name"
                    );
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityid);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            if (!_cityInfoRepository.cityExist(cityid))
            {
                return NotFound();
            }


            //var maxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.id);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    id = ++maxPointOfInterestId,
            //    Name = pointOfInterst.Name,
            //    Description = pointOfInterst.Description
            //};

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterst);

            _cityInfoRepository.AddPointOfInterestForCity(cityid, finalPointOfInterest);

            _cityInfoRepository.save();

            var createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);
                
            //city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityid = cityid, id = createdPointOfInterestToReturn.id}, createdPointOfInterestToReturn);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePointOfIntrest(int cityid, int id, PointOfInterestForUpdatingDto pointOfInterst)
        {

            if (pointOfInterst.Description == pointOfInterst.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided Description should be different from the name"
                    );
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.cityExist(cityid))
            {
                return NotFound();
            }

            //var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.id == id);

            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}
            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityid, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterst, pointOfInterestEntity);

            _cityInfoRepository.UpdatePointOfInterest(cityid, pointOfInterestEntity);

            _cityInfoRepository.save();


            //pointOfInterestFromStore.Name = pointOfInterst.Name;
            //pointOfInterestFromStore.Description = pointOfInterst.Description;


            return NoContent();

        }



        [HttpPatch("{id}")]
        public IActionResult partiallyUpdatedPointOfIntrest(int cityid, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdatingDto> patchDoc)
        {


            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityid);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.id == id);

            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}


            if (!_cityInfoRepository.cityExist(cityid))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityid, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            //var pointOfInterestToPatch = new PointOfInterestForUpdatingDto()
            //{
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description
            //};
            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdatingDto>(pointOfInterestEntity);



            patchDoc.ApplyTo(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided Description should be different from the name"
                    );
            }


            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return NotFound();
            }


            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            _cityInfoRepository.UpdatePointOfInterest(cityid, pointOfInterestEntity);

            _cityInfoRepository.save();

            return NoContent();
        }


        [HttpDelete("{id}")]

        public IActionResult DeletePointOfIntrest(int cityid, int id)
        {

            if (!_cityInfoRepository.cityExist(cityid))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityid, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }


            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            _cityInfoRepository.save();


            //city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _mailService.send("Point of interest deleted.",
                $"POint of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.id}"
                );

            return NoContent();
        }

    }
}
