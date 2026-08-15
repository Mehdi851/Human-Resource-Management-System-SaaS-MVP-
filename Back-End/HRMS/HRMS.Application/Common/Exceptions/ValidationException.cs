using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("Validation failed.")
        {
            Errors = errors.ToList();
        }

        public ValidationException(string error)
            : this(new[] { error })
        {
        }
    }
}
