using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.Services;

namespace TenantExercise3.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly OperationIdService _operationIdService;
        private readonly IDataService _dataService;

        public ValuesController(IDataService dataService, OperationIdService operationIdService)
        {
            _operationIdService = operationIdService;
            _dataService = dataService;
        }
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> GetOperationIdValue()
        {
            return Ok(_operationIdService.Id);
        }
    }
}
