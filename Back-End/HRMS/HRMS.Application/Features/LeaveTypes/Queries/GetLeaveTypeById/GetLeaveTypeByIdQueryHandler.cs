using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.LeaveTypes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    public class GetLeaveTypeByIdQueryHandler
    : IRequestHandler<GetLeaveTypeByIdQuery, LeaveTypeDetailsDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeByIdQueryHandler(
            ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailsDto> Handle(
            GetLeaveTypeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(
                request.Id);

            if (leaveType is null ||
                leaveType.IsDeleted ||
                leaveType.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Leave type not found.");
            }

            return new LeaveTypeDetailsDto
            {
                Id = leaveType.Id,
                OrganizationId = leaveType.OrganizationId,
                Name = leaveType.Name,
                Description = leaveType.Description,
                IsPaid = leaveType.IsPaid,
                DefaultDays = leaveType.DefaultDays
            };
        }
    }
}
