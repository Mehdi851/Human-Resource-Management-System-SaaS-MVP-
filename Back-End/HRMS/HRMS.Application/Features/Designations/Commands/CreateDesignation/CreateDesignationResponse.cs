using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public Guid OrganizationId { get; set; }
        public string Message { get; internal set; }
    }
}
