using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.Models;

namespace TenantExercise3.Tenancy
{
    internal interface ITenantResolver
    {
        Task<Tenant> ResolveAsync(object tenantId);
    }
}
