using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommandHandler
    : IRequestHandler<UpdateAttendanceCommand, UpdateAttendanceResponse>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAttendanceCommandHandler(
            IAttendanceRepository attendanceRepository,
            IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateAttendanceResponse> Handle(
            UpdateAttendanceCommand request,
            CancellationToken cancellationToken)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(
                request.Id);

            if (attendance is null ||
                attendance.IsDeleted ||
                attendance.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Attendance record was not found in the specified organization.");
            }

            // Employee and attendance date are intentionally immutable.
            attendance.CheckInTime = request.CheckInTime;
            attendance.CheckOutTime = request.CheckOutTime;
            attendance.Status = request.Status;
            attendance.Remarks = request.Remarks;

            // Always recalculate derived working hours after time changes.
            attendance.CalculateWorkingHours();

            _attendanceRepository.Update(attendance);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new UpdateAttendanceResponse
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,
                AttendanceDate = attendance.AttendanceDate,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                Status = attendance.Status,
                WorkingHours = attendance.WorkingHours,
                Remarks = attendance.Remarks
            };
        }
    }
}
