using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.UpdateDesignation
{
    public class UpdateDesignationCommandHandler
    : IRequestHandler<UpdateDesignationCommand, UpdateDesignationResponse>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDesignationCommandHandler(
            IDesignationRepository designationRepository,
            IUnitOfWork unitOfWork)
        {
            _designationRepository = designationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateDesignationResponse> Handle(
            UpdateDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var designation = await _designationRepository.GetByIdAsync(
                request.Id);

            if (designation is null)
            {
                throw new KeyNotFoundException(
                    "Designation not found.");
            }

            // Prevent duplicate designation names within the same organization
            // while excluding the designation currently being updated.
            var designationExists =
                await _designationRepository.DesignationNameExistsAsync(
                    request.OrganizationId,
                    request.Name.Trim(),
                    request.Id,
                    cancellationToken);

            if (designationExists)
            {
                throw new InvalidOperationException(
                    "A designation with the same name already exists.");
            }

            designation.OrganizationId = request.OrganizationId;
            designation.Name = request.Name.Trim();
            designation.Description = request.Description?.Trim();

            _designationRepository.Update(designation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateDesignationResponse
            {
                Id = designation.Id,
                OrganizationId = designation.OrganizationId,
                Name = designation.Name,
                Description = designation.Description
            };
        }
    }
}
