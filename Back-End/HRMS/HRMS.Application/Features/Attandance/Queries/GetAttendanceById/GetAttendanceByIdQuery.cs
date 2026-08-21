using HRMS.Application.Features.Attandance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendanceById
{
    public class GetAttendanceByIdQuery
    : IRequest<AttendanceDetailsDto>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
