using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using BLL.Infrastructure;
using BLL.ModelsDTO;
using SocialNetwork.Models;

namespace SocialNetwork.Tools
{
    public class AutoMapperModule :NinjectModule
    {
        public override void Load()
        {
            //Bind<IValueResolver<SourceEntity, DestModel, bool>>().To<MyResolver>();

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfile<AutoMapperProfile>();
                cfg.CreateMap<SearchParams, QueryParams>();

            });

            return config;
        }
    }
}