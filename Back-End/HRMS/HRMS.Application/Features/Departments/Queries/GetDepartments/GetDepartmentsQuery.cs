using HRMS.Application.Common.Models;
using HRMS.Application.Features.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQuery
       : IRequest<PagedResponse<DepartmentListItemDto>>
    {
        //----------------------------------
        // Pagination
        //----------------------------------

        /// <summary>
        /// Current page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of records per page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        //----------------------------------
        // Search
        //----------------------------------

        /// <summary>
        /// Search text for department name or description.
        /// </summary>
        public string Search { get; set; } = string.Empty;

        //----------------------------------
        // Filter
        //----------------------------------

        /// <summary>
        /// Filter departments by organization.
        /// </summary>
        public Guid? OrganizationId { get; set; }

        //----------------------------------
        // Sorting
        //----------------------------------

        /// <summary>
        /// Property used for sorting.
        /// Default is Name.
        /// </summary>
        public string SortBy { get; set; } = "Name";

        /// <summary>
        /// True for descending order.
        /// False for ascending order.
        /// </summary>
        public bool Descending { get; set; } = false;
    }

}
