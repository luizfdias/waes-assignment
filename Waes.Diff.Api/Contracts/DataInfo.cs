﻿using System;
using Waes.Diff.Api.Contracts.Enums;

namespace Waes.Diff.Api.Contracts
{
    public class DataInfo
    {
        public Guid Id { get; set; }

        public string CorrelationId { get; set; }

        public int Length { get; set; }

        public SideEnum Side { get; set; }
    }
}
