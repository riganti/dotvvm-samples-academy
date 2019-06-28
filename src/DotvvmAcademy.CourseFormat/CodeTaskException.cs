using System;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskException : Exception
    {
        public CodeTaskException(string message) : base(message)
        {
        }

        public CodeTaskException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}