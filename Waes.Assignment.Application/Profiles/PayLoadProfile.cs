using AutoMapper;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Profiles
{
    public class PayLoadProfile : Profile
    {
        public PayLoadProfile()
        {
            CreateMap<CreateLeftPayLoadRequest, PayLoadCreateCommand>()
                .ConstructUsing((src, ctx) => new PayLoadCreateCommand(ctx.Items["correlationId"].ToString(), src.Content, SideEnum.Left));

            CreateMap<CreateRightPayLoadRequest, PayLoadCreateCommand>()
                .ConstructUsing((src, ctx) => new PayLoadCreateCommand(ctx.Items["correlationId"].ToString(), src.Content, SideEnum.Right));

            CreateMap<PayLoadCreatedEvent, CreatePayLoadResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AggregateId));            
        }
    }
}
