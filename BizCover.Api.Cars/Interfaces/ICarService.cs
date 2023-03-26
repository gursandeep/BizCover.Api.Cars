using BizCover.Api.Cars.ContractModels;
using BizCover.Api.Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetCars();
        Task<Car> GetCar(int carId);
        Task<Car> AddCar(Car car);
        Task<Car> UpdateCar(Car car);
        Task<decimal> CalculateDiscount(IEnumerable<int> carIds);
    }
}
