using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.Models;

namespace TenantExercise3.Respository
{
    public interface ITenantStore<T> where T : Tenant
    {
        Task<T> GetTenantAsync(string identifier);
    }
}
