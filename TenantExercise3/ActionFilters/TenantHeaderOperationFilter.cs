using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantExercise3.ActionFilters
{
    /// <summary>
    /// Adds Tenant Id field to API endpoints
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    public class TenantHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            if (context.ApiDescription.RelativePath.Contains("api"))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "tenantid",
                    In = ParameterLocation.Header,
                    Description = "Tenant Id",
                    Required = true,
                });
            }
        }
    }
}
