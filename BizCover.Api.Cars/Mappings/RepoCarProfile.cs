using AutoMapper;
using BizCover.Api.Cars.ContractModels;

namespace BizCover.Api.Cars.Mappings
{
    public class RepoCarProfile : Profile
    {
        public RepoCarProfile()
        {
            CreateMap<Repository.Cars.Car, Car>().ReverseMap();
        }
    }
}