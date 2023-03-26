using AutoMapper;
using BizCover.Api.Cars.App_Start;
using BizCover.Api.Cars.ContractModels;
using BizCover.Api.Cars.Interfaces;
using BizCover.Api.Cars.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BizCover.Api.Cars.Controllers
{
    public class CarsController : ApiController
    {
        // Below is just the sample code from the Visual Studio Web Api Template. 
        // Feel free to replace this with whatever implementation you feel is suitable and production ready for a web api.

        // Calling the 3rd party api is expensive and its data only gets updated every 24 hours. Caching can help with this.

        // The repository BizCover.Repository.Cars can be found in ../packages/BizCover.Repository.Cars.1.0.0/BizCover.Repository.Cars.dll. You can restructure this solution as you like.

        private readonly ICarService _carService;
        private readonly IMapper mapper = AutoMapperConfig._mapper;
        

        public CarsController(ICarService carService)
        {
            _carService = carService;

        }

        // GET api/values
        [HttpGet]
        [Route("api/cars/getallcars")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cars retrieved successfully.", Type = typeof(List<CarDTO>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Failed to retrieve cars.", Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var cars = await _carService.GetCars();
                var returnCars = mapper.Map<List<Car>, List<CarDTO>>(cars);
                return Ok(returnCars);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/cars/{carId}/getcar", Name = "GetCar")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Car retrieved successfully.", Type = typeof(CarDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Car not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Failed to retrieve car.", Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> GetCar([FromUriAttribute] int carId)
        {
            try
            {
                var car = await _carService.GetCar(carId);
                if (car != null)
                {
                    return Ok(car);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/cars/add")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Car created successfully.", Type = typeof(CarDTO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Failed to create car.", Type = typeof(InternalServerErrorResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid input.", Type = typeof(CarDTO))]
        public async Task<IHttpActionResult> Post([FromBodyAttribute] CarDTO car)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCard = await _carService.AddCar(mapper.Map<CarDTO, Car>(car));

                return CreatedAtRoute("GetCar", new { carId = createdCard.Id }, createdCard);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPut]
        [Route("api/cars/update")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Car updated successfully.", Type = typeof(CarDTO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Failed to update car.", Type = typeof(InternalServerErrorResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid input.", Type = typeof(CarDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Car not found.")]
        public async Task<IHttpActionResult> Put([FromBodyAttribute] CarDTO car)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedCar = await _carService.UpdateCar(mapper.Map<CarDTO, Car>(car));

                if(updatedCar == null)
                {
                    return NotFound();
                }

                return Ok(updatedCar);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/cars/discount")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Discount calculated successfully.", Type = typeof(decimal))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Failed to calculate discount.", Type = typeof(InternalServerErrorResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid input.", Type = typeof(string))]
        public async Task<IHttpActionResult> Discount([FromUri] List<int> carIds)
        {
            try
            {
                if (carIds == null || carIds.Count < 1)
                {
                    return BadRequest("Atleast one CarId is required.");
                }
                var discount = await _carService.CalculateDiscount(carIds);

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
