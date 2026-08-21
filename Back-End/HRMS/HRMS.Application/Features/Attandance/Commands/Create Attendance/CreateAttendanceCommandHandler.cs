using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.Create_Attendance
{
    public class CreateAttendanceCommandHandler
    : IRequestHandler<CreateAttendanceCommand, CreateAttendanceResponse>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttendanceCommandHandler(
            IAttendanceRepository attendanceRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateAttendanceResponse> Handle(
            CreateAttendanceCommand request,
            CancellationToken cancellationToken)
        {
            // Validate employee existence and organization ownership.
            var employee = await _employeeRepository.GetByIdAsync(
                request.EmployeeId);

            if (employee is null ||
                employee.IsDeleted ||
                employee.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Employee was not found in the specified organization.");
            }

            // Prevent duplicate attendance for the same employee/date.
            var attendanceExists =
                await _attendanceRepository.AttendanceExistsAsync(
                    request.EmployeeId,
                    request.AttendanceDate,
                    cancellationToken: cancellationToken);

            if (attendanceExists)
            {
                throw new InvalidOperationException(
                    "Attendance already exists for this employee on the specified date.");
            }

            // Check-out cannot be earlier than check-in.
            if (request.CheckInTime.HasValue &&
                request.CheckOutTime.HasValue &&
                request.CheckOutTime.Value < request.CheckInTime.Value)
            {
                throw new InvalidOperationException(
                    "Check-out time cannot be earlier than check-in time.");
            }

            var attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                OrganizationId = request.OrganizationId,
                EmployeeId = request.EmployeeId,
                AttendanceDate = request.AttendanceDate,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                Status = request.Status,
                Remarks = request.Remarks
            };

            // WorkingHours is calculated by the domain entity.
            attendance.CalculateWorkingHours();

            await _attendanceRepository.AddAsync(
                attendance);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new CreateAttendanceResponse
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,
                AttendanceDate = attendance.AttendanceDate,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                Status = attendance.Status,
                WorkingHours = attendance.WorkingHours
            };
        }
    }
}
