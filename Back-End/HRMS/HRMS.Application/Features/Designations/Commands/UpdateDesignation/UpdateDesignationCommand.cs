using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.UpdateDesignation
{
    public class UpdateDesignationCommand : IRequest<UpdateDesignationResponse>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
