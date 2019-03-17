﻿using System.Threading.Tasks;
using Waes.Assignment.Application.ViewModels;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IDiffAnalyzerService
    {
        Task<DiffResponse> Analyze(string correlationId);
    }
}