using AutoMapper;
using System;
using System.Collections.Generic;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Application.Profiles
{
    public class DiffProfile : Profile
    {
        public DiffProfile()
        {
            CreateMap<Diff, DiffResponse>().ConvertUsing<DiffResponseConverter>();
            CreateMap<DiffSequence, DiffInfoResponse>();
        }
    }

    //TODO: nao sei se gostei disso aqui.
    public class DiffResponseConverter : ITypeConverter<Diff, DiffResponse>
    {        
        public DiffResponse Convert(Diff source, DiffResponse destination, ResolutionContext context)
        {
            switch (source.Info.Status)
            {
                case DiffStatus.Equal:
                    return new EqualResponse();
                case DiffStatus.NotEqual:
                    return new NotEqualResponse(context.Mapper.Map<IEnumerable<DiffInfoResponse>>(source.Info.GetSequenceOfDifferences()));
                case DiffStatus.NotOfEqualSize:
                    return new NotOfEqualSizeResponse();
                default:
                    throw new InvalidOperationException($"Enum {source.Info.Status} not supported");
            }
        }
    }
}
