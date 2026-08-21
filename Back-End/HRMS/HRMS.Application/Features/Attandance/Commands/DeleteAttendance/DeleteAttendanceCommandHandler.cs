using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.DeleteAttendance
{
    public class DeleteAttendanceCommandHandler
    : IRequestHandler<DeleteAttendanceCommand, bool>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAttendanceCommandHandler(
            IAttendanceRepository attendanceRepository,
            IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            DeleteAttendanceCommand request,
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

            // Soft delete only. Historical attendance data remains in the database.
            attendance.IsDeleted = true;

            _attendanceRepository.Update(attendance);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }
    }
}
