using AutoMapper;
using System.Collections.Generic;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Profiles
{
    public class DiffProfile : Profile
    {
        public DiffProfile()
        {
            CreateMap<Diff, DiffResponse>().ConvertUsing(new DiffResponseConverter());

            CreateMap<DiffInfo, DiffInfoResponse>();            
        }
    }

    public class DiffResponseConverter : ITypeConverter<Diff, DiffResponse>
    {
        public DiffResponse Convert(Diff source, DiffResponse destination, ResolutionContext context)
        {
            if (!source.HasDiff())
                return new EqualResponse();

            return new NotEqualResponse(context.Mapper.Map<IEnumerable<DiffInfo>, List<DiffInfoResponse>>(source.Info));
        }
    }
}
