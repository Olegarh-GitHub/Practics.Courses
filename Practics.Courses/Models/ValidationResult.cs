using System;

namespace Practics.Courses.Models
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }

        public ValidationResult(bool success)
        {
            Success = success;
        }

        public ValidationResult(Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public ValidationResult(string error)
        {
            Success = false;
            Exception = new Exception(error);
        }
    }
}