using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Queries.GetDesignationById
{
    public class DesignationDetailsDto
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
