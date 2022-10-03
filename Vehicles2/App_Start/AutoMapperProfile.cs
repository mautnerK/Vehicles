using AutoMapper;
using Vehicles.Model.Common;
using Vehicles.Models;

namespace Vehicles.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IMake, MakeViewModel>();
            CreateMap<IModel, ModelViewModel>();
            CreateMap<MakeViewModel, IMake>();
            CreateMap<ModelViewModel, IModel>();
        }
    }
}