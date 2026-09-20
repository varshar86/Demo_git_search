using System;
using System.Text.Json;
using System.Xml.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Searchify.Application.Queries;
using Searchify.Domain.Model;
using System.IO;

namespace Searchify.API.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
       

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);

                if (result == null || !result.Any())
                {
                    return NotFound(new { Message = "No Products found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Detail = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound(new { Message = $"Product with id {id} not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "An unexpected error occurred.", Detail = ex.Message });
            }
        }

       


    }
}

