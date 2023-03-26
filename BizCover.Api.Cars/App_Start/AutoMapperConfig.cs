using AutoMapper;
using BizCover.Api.Cars.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizCover.Api.Cars.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper _mapper { get; set; }

        public static void Register()
        {
            var mapperConfig = new MapperConfiguration(
                config =>
                {
                    config.AddProfile<CarProfile>();
                    config.AddProfile<RepoCarProfile>();
                }
            );

            _mapper = mapperConfig.CreateMapper();
        }

    }
}