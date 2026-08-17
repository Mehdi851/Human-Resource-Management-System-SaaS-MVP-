using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationCommand : IRequest<CreateDesignationResponse>
    {
        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
