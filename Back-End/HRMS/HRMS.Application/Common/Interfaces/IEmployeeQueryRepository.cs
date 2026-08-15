using HRMS.Application.Features.Employees.Queries.GetEmployeeById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IEmployeeQueryRepository
    {
        Task<GetEmployeeByIdResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        //Task<PagedResult<EmployeeListItemDto>> GetPagedAsync(
        //    EmployeeFilter filter,
        //    CancellationToken cancellationToken = default);
    }
}
