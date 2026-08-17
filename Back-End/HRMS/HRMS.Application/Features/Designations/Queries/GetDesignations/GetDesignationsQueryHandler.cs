using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Queries.GetDesignations
{
    public class GetDesignationsQueryHandler
    : IRequestHandler<GetDesignationsQuery, PagedResponse<DesignationListDto>>
    {
        private readonly IDesignationRepository _designationRepository;

        public GetDesignationsQueryHandler(
            IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<PagedResponse<DesignationListDto>> Handle(
            GetDesignationsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _designationRepository.GetPagedAsync(
                request.OrganizationId,
                request.Search,
                request.SortBy,
                request.SortDescending,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = result.Items
                .Select(x => new DesignationListDto
                {
                    Id = x.Id,
                    OrganizationId = x.OrganizationId,
                    Name = x.Name,
                    Description = x.Description
                })
                .ToList();

            return new PagedResponse<DesignationListDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                //TotalCount = result.TotalCount
            };
        }
    }
}
