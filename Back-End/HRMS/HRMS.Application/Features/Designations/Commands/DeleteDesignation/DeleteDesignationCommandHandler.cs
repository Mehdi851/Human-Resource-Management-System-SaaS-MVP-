using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.DeleteDesignation
{
    public class DeleteDesignationCommandHandler
    : IRequestHandler<DeleteDesignationCommand, bool>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDesignationCommandHandler(
            IDesignationRepository designationRepository,
            IUnitOfWork unitOfWork)
        {
            _designationRepository = designationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            DeleteDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var designation = await _designationRepository.GetByIdAsync(
                request.Id);

            if (designation is null)
            {
                throw new KeyNotFoundException(
                    "Designation not found.");
            }

            _designationRepository.Delete(designation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
