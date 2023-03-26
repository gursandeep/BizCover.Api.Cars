using AutoMapper;
using BizCover.Api.Cars.ContractModels;
using BizCover.Api.Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizCover.Api.Cars.Mappings
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
        }
    }
}