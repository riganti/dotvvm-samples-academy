using System;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskCompilationException : Exception
    {
        public CodeTaskCompilationException(Exception inner) 
            : base("An exception occured during compilation of a validation script.", inner)
        {
        }
    }
}