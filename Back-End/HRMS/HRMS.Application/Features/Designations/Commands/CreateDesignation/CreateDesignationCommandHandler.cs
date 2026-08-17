using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationCommandHandler
     : IRequestHandler<CreateDesignationCommand, CreateDesignationResponse>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDesignationCommandHandler(
            IDesignationRepository designationRepository,
            IUnitOfWork unitOfWork)
        {
            _designationRepository = designationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDesignationResponse> Handle(
            CreateDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var designationExists =
                await _designationRepository.DesignationNameExistsAsync(
                    request.OrganizationId,
                    request.Name.Trim(),
                    cancellationToken: cancellationToken);

            if (designationExists)
            {
                throw new InvalidOperationException(
                    "A designation with the same name already exists.");
            }

            var designation = new Designation
            {
                OrganizationId = request.OrganizationId,
                Name = request.Name.Trim(),
                Description = request.Description?.Trim()
            };

            await _designationRepository.AddAsync(
                designation
               );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateDesignationResponse
            {
                Id = designation.Id,
                Name = designation.Name,
                OrganizationId = designation.OrganizationId
            };
        }
    }
}
