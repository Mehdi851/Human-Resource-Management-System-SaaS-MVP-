using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Queries.GetDesignationById
{
    public class GetDesignationByIdQuery
    : IRequest<DesignationDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
