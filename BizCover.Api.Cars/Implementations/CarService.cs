using AutoMapper;
using BizCover.Api.Cars.App_Start;
using BizCover.Api.Cars.ContractModels;
using BizCover.Api.Cars.Interfaces;
using BizCover.Api.Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BizCover.Api.Cars.Implementations
{
    public class CarService : ICarService
    {
        private readonly Repository.Cars.CarRepository _repo;
        private readonly IMapper mapper = AutoMapperConfig._mapper;
        private const string CacheKey = "Cars";
        public CarService()
        {
            _repo = new Repository.Cars.CarRepository();
        }

        public async Task<Car> AddCar(Car car)
        {

            var repoCar = mapper.Map<Car, Repository.Cars.Car>(car);
            await _repo.Add(repoCar);
            return mapper.Map<Repository.Cars.Car, Car>(repoCar);
        }

        public async Task<decimal> CalculateDiscount(IEnumerable<int> carIds)
        {
            decimal discount = 0M;
            decimal discountPercentage = 0M;
            var discountedCars = new List<Repository.Cars.Car>();
            var cars = MemoryCacher.GetValue(CacheKey) as List<Repository.Cars.Car>;
            if (cars == null)
            {
                var repoCars = await _repo.GetAllCars();
                MemoryCacher.Add(CacheKey, repoCars);
                cars = repoCars;
            }

            foreach (var carId in carIds)
            {
                var car = cars.Where(c => c.Id == carId).FirstOrDefault();
                if (car != null)
                {
                    discountedCars.Add(car);
                }
            }

            if (discountedCars.Count > 2)
            {
                discountPercentage = discountPercentage + 0.03M;
            }

            if (discountedCars.Sum(x => x.Price) > 100000)
            {
                discountPercentage = discountPercentage + 0.05M;
            }

            foreach (var discountedCar in discountedCars)
            {
                if (discountedCar.Year < 2000)
                {
                    discountedCar.Price = 0.1M * discountedCar.Price;
                }
            }

            var totalCarPrice = discountedCars.Sum(x => x.Price);

            discount = (totalCarPrice * discountPercentage);


            return discount;
        }

        public async Task<List<Car>> GetCars()
        {
            var cachedCars = MemoryCacher.GetValue(CacheKey) as List<Repository.Cars.Car>;
            if (cachedCars == null)
            {
                var repoCars = await _repo.GetAllCars();
                MemoryCacher.Add(CacheKey, repoCars);
                cachedCars = repoCars;
            }
            var cars = mapper.Map<List<Repository.Cars.Car>, List<Car>>(cachedCars);
            return cars;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            var selectedCar = await GetCar(car.Id);

            if (selectedCar == null)
            {
                return null;
            }

            selectedCar.Make = car.Make;
            selectedCar.Model = car.Model;
            selectedCar.Price = car.Price;
            selectedCar.Colour = car.Colour;
            selectedCar.CountryManufactured = car.CountryManufactured;
            selectedCar.Year = car.Year;

            var repoCar = mapper.Map<Car, Repository.Cars.Car>(selectedCar);
            await _repo.Update(repoCar);
            return mapper.Map<Repository.Cars.Car, Car>(repoCar);
        }

        public async Task<Car> GetCar(int carId)
        {
            var cachedCars = await GetCars();
            var car = cachedCars.FirstOrDefault(c => c.Id == carId);
            return car;
        }
    }

}