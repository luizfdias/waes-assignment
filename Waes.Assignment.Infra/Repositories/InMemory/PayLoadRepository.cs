﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Infra.Repositories.InMemory
{
    public class PayLoadRepository : Repository<PayLoad>, IPayLoadRepository
    {
        public PayLoadRepository(InMemoryDatabase<PayLoad> database) : base(database)
        {
        }

        public async Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid)
        {
            return Database.Entities.Where(x => x.CorrelationId == correlationid);
        }

        public async Task<PayLoad> GetByCorrelationIdAndSide(string correlationid, SideEnum side)
        {
            return Database.Entities.FirstOrDefault(x => x.CorrelationId == correlationid && x.Side == side);
        }
    }
}