﻿using System.Threading.Tasks;
using Waes.Assignment.Api.ViewModels;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IPayLoadService
    {
        Task<CreatePayLoadResponse> Create(string correlationId, CreatePayLoadRequest request);
    }
}