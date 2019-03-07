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
                .Include<CreateLeftPayLoadRequest, PayLoad>()
                .Include<CreateRightPayLoadRequest, PayLoad>();                            

            CreateMap<CreateLeftPayLoadRequest, PayLoad>()
                .ConstructUsing((x, context)
                => new PayLoad(
                    Guid.NewGuid(),
                    context.Items["correlationId"].ToString(),
                    x.Content,
                    SideEnum.Left));
            
            CreateMap<CreateRightPayLoadRequest, PayLoad>()
                .ConstructUsing((x, context)
                => new PayLoad(
                    Guid.NewGuid(),
                    context.Items["correlationId"].ToString(),
                    x.Content,
                    SideEnum.Right));

            CreateMap<PayLoad, CreatePayLoadResponse>();
        }
    }
}
