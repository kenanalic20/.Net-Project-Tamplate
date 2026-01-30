using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamplate.Application.DTOs;
using Tamplate.Application.Filters;
using Tamplate.Application.Interfaces.Services;
using Tamplate.Domain.Models;

namespace Tamplate.API.Controllers
{
    //If you want to authorize controller
    // [Authorize]
    [ApiController]
    public class ExampleController : BaseController<Example, ExampleDto, ExampleCreateDto, ExampleUpdateDto, ExampleQueryFilter, int>
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService) 
            : base(exampleService)
        {
            _exampleService = exampleService;
        }

        // All basic CRUD operations are inherited from BaseController:
        // - GET /Example/{id} - GetById
        // - GET /Example - GetAll with optional query filter
        // - POST /Example - Create
        // - PUT /Example/{id} - Update
        // - DELETE /Example/{id} - Delete

        // You can add custom endpoints here if needed
        // Example:
        // [HttpGet("custom")]
        // public async Task<IActionResult> CustomEndpoint()
        // {
        //     // Custom logic
        //     return Ok();
        // }
    }
}
