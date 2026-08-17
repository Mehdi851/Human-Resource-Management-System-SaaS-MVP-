using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.DeleteDesignation
{
    public class DeleteDesignationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
