using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DotvvmAcademy.Services
{
	public class LessonProgressStorage
	{
		public const int FinishedLessonStepNumber = 999;
		private const string ProgressCookieName = "dotvvmAcademyProgress";
		private readonly HttpContext httpContext;

		public LessonProgressStorage(HttpContext httpContext)
		{
			this.httpContext = httpContext;
		}

		public int GetLessonLastStep(int number)
		{
			if (!httpContext.Request.Cookies.TryGetValue(ProgressCookieName, out string cookie))
			{
				return 1;
			}

			var parts = cookie.Split(',');
			if (number - 1 < parts.Length)
			{
				return int.Parse(parts[number - 1]);
			}
				
			return 1;
		}

		public void UpdateLessonLastStep(int number, int step)
		{
			List<int> parts;
			if (!httpContext.Request.Cookies.TryGetValue(ProgressCookieName, out string cookie))
				parts = Enumerable.Range(0, number).Select(p => 1).ToList();
			else
				parts = cookie.Split(',').Select(int.Parse).ToList();

			while (number >= parts.Count)
				parts.Add(1);

			if (parts[number - 1] < step)
				parts[number - 1] = step;

			httpContext.Response.Cookies.Append(ProgressCookieName, string.Join(",", parts), new CookieOptions
			{
				Expires = DateTime.Today.AddYears(1)
			});
		}
	}
}