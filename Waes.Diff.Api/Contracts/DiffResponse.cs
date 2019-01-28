﻿using System.Collections.Generic;

namespace Waes.Diff.Api.Contracts
{
    public class DiffResponse
    {
        public bool EqualsSize { get; set; }

        public IEnumerable<DataInfo> DataInfo { get; set; }

        public IEnumerable<Difference> Differences { get; set; }
    }
}