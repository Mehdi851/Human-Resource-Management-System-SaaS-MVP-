using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQueryHandler
        : IRequestHandler<GetDepartmentsQuery, PagedResponse<DepartmentListItemDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentsQueryHandler(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<PagedResponse<DepartmentListItemDto>> Handle(
            GetDepartmentsQuery request,
            CancellationToken cancellationToken)
        {
            // Repository handles filtering, searching, sorting and pagination.
            return await _departmentRepository.GetPagedDepartmentsAsync(
                request,
                cancellationToken);
        }
    }
}
