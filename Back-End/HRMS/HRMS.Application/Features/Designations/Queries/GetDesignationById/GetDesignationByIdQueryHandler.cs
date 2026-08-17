using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Queries.GetDesignationById
{
    public class GetDesignationByIdQueryHandler
    : IRequestHandler<GetDesignationByIdQuery, DesignationDetailsDto>
    {
        private readonly IDesignationRepository _designationRepository;

        public GetDesignationByIdQueryHandler(
            IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<DesignationDetailsDto> Handle(
            GetDesignationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var designation = await _designationRepository.GetByIdAsync(
                request.Id);

            if (designation is null)
            {
                throw new KeyNotFoundException(
                    "Designation not found.");
            }

            return new DesignationDetailsDto
            {
                Id = designation.Id,
                OrganizationId = designation.OrganizationId,
                Name = designation.Name,
                Description = designation.Description
            };
        }
    }
}
