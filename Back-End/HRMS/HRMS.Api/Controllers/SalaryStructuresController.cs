using HRMS.Application.Features.Payroll.SalaryStructures.Commands.CreateSalaryStructure;
using HRMS.Application.Features.Payroll.SalaryStructures.Commands.UpdateSalaryStructure;
using HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructureById;
using HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryStructuresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaryStructuresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSalaryStructureCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetSalaryStructureByIdQuery
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid organizationId,
            [FromQuery] Guid? employeeId,
            CancellationToken cancellationToken)
        {
            var query = new GetSalaryStructuresQuery
            {
                OrganizationId = organizationId,
                EmployeeId = employeeId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateSalaryStructureCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
