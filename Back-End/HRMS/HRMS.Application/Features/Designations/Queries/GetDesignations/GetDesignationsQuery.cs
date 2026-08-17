using HRMS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Queries.GetDesignations
{
    public class GetDesignationsQuery
    : IRequest<PagedResponse<DesignationListDto>>
    {
        public Guid OrganizationId { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
