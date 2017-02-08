using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Lessons
{
	public abstract class LessonBase
	{
		public IEnumerable<IStep> Steps { get; set; }
	}
}