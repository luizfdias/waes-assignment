using AutoMapper;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Application.Profiles
{
    public class DiffProfile : Profile
    {
        public DiffProfile()
        {
            CreateMap<DifferenceInterval, DiffInfoResponse>();

            CreateMap<EqualDiff, EqualResponse>();
            CreateMap<NotOfEqualSizeDiff, NotOfEqualSizeResponse>();
            CreateMap<NotEqualDiff, NotEqualResponse>()
                .ForMember(dest => dest.Info, opt => opt.MapFrom(src => src.Differences));

            CreateMap<Diff, DiffResponse>()
                .Include<EqualDiff, EqualResponse>()
                .Include<NotOfEqualSizeDiff, NotOfEqualSizeResponse>()
                .Include<NotEqualDiff, NotEqualResponse>();
        }
    }    
}
