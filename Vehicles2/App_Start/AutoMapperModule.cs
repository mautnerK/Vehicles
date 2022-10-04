using AutoMapper;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vehicles.Model.Common;
using Vehicles.Models;

namespace Vehicles2.App_Start
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IMake, MakeViewModel>(); cfg.CreateMap<IModel, ModelViewModel>();
                cfg.CreateMap<MakeViewModel, IMake>(); cfg.CreateMap<ModelViewModel, IModel>();
            });
            this.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            this.Bind<Root>().ToSelf().InSingletonScope();
        }

        public class Root
        {
            public Root(IMapper mapper)
            {
            }
        }
    }
}