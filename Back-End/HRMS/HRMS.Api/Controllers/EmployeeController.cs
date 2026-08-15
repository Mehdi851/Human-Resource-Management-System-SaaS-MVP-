using HRMS.Application.Common.Helpers;
using HRMS.Application.Features.Employees.Commands.CreateEmployee;
using HRMS.Application.Features.Employees.Commands.DeleteEmployee;
using HRMS.Application.Features.Employees.Commands.UpdateEmployee;
using HRMS.Application.Features.Employees.Queries.GetEmployeeById;
using HRMS.Application.Features.Employees.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ResponseFactory.Success(
                    result,
                    "Employee created successfully."));
        }

        // GET: api/employee/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetEmployeeByIdQuery { Id = id });

            return Ok(
            ResponseFactory.Success(
                result,
                "Employee retrieved successfully."));
                
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetEmployeesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(
                ResponseFactory.Success(
                    result,
                    "Employees retrieved successfully."));
        }

        // PUT: api/employee/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(
                ResponseFactory.Success(
                    result,
                    "Employee updated successfully."));
        }
        // DELETE: api/employee/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                new DeleteEmployeeCommand { Id = id });

            return Ok(
                ResponseFactory.Success(
                    result,
                    "Employee deleted successfully."));
        }
    }
}
