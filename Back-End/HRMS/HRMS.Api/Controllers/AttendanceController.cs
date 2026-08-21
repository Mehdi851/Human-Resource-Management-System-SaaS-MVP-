using HRMS.Application.Features.Attandance.Commands.Create_Attendance;
using HRMS.Application.Features.Attandance.Commands.DeleteAttendance;
using HRMS.Application.Features.Attandance.Commands.UpdateAttendance;
using HRMS.Application.Features.Attandance.Queries.GetAttendanceByDate;
using HRMS.Application.Features.Attandance.Queries.GetAttendanceById;
using HRMS.Application.Features.Attandance.Queries.GetAttendances;
using HRMS.Application.Features.Attandance.Queries.GetEmployeeAttendance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateAttendanceCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAttendancesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetAttendanceByIdQuery
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("employee/{employeeId:guid}")]
        public async Task<IActionResult> GetEmployeeAttendance(
            Guid employeeId,
            [FromQuery] Guid organizationId,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate,
            [FromQuery] HRMS.Domain.Enums.AttendanceStatus? status,
            [FromQuery] string? sortBy,
            [FromQuery] bool sortDescending = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetEmployeeAttendanceQuery
            {
                EmployeeId = employeeId,
                OrganizationId = organizationId,
                FromDate = fromDate,
                ToDate = toDate,
                Status = status,
                SortBy = sortBy,
                SortDescending = sortDescending,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(
            DateOnly date,
            [FromQuery] Guid organizationId,
            [FromQuery] Guid? departmentId,
            [FromQuery] Guid? employeeId,
            [FromQuery] HRMS.Domain.Enums.AttendanceStatus? status,
            [FromQuery] string? sortBy,
            [FromQuery] bool sortDescending = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAttendanceByDateQuery
            {
                OrganizationId = organizationId,
                AttendanceDate = date,
                DepartmentId = departmentId,
                EmployeeId = employeeId,
                Status = status,
                SortBy = sortBy,
                SortDescending = sortDescending,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateAttendanceCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteAttendanceCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
