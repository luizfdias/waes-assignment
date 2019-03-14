using AutoMapper;
using System;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Profiles
{
    public class PayLoadProfile : Profile
    {
        public PayLoadProfile()
        {
            CreateMap<CreatePayLoadRequest, PayLoad>()
                .ForMember(dest => dest.CorrelationId, opt => opt.MapFrom((src, dest, s, ctx) => ctx.Items["correlationId"]))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Side, opt => opt.Ignore())
                .Include<CreateLeftPayLoadRequest, PayLoad>()
                .Include<CreateRightPayLoadRequest, PayLoad>();

            CreateMap<CreateLeftPayLoadRequest, PayLoad>()
                .ForMember(dest => dest.Side, opt => opt.MapFrom(src => SideEnum.Left));

            CreateMap<CreateRightPayLoadRequest, PayLoad>()
                .ForMember(dest => dest.Side, opt => opt.MapFrom(src => SideEnum.Right));

            CreateMap<PayLoad, CreatePayLoadResponse>();
        }
    }
}
