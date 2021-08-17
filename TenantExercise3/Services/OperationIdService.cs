using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantExercise3.Services
{
    public class OperationIdService
    {
        public readonly Guid Id;

        public OperationIdService()
        {
            Id = Guid.NewGuid();
        }
    }
}
