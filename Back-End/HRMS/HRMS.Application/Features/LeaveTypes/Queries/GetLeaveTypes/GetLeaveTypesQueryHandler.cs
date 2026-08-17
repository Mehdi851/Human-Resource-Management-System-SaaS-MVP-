using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.LeaveTypes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypes
{
    public class GetLeaveTypesQueryHandler
    : IRequestHandler<GetLeaveTypesQuery, PagedResponse<LeaveTypeListDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypesQueryHandler(
            ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<PagedResponse<LeaveTypeListDto>> Handle(
            GetLeaveTypesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _leaveTypeRepository.GetPagedAsync(
                request.OrganizationId,
                request.PageNumber,
                request.PageSize,
                request.Search,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            var items = result.Items
                .Select(x => new LeaveTypeListDto
                {
                    Id = x.Id,
                    OrganizationId = x.OrganizationId,
                    Name = x.Name,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                    DefaultDays = x.DefaultDays
                })
                .ToList();

            return new PagedResponse<LeaveTypeListDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                //TotalCount = result.TotalCount
            };
        }
    }
}
