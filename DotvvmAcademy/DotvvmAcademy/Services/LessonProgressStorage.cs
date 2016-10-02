using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Services
{
    public class LessonProgressStorage
    {
        private const string ProgressCookieName = "dotvvmAcademyProgress";
        private HttpContext httpContext;

        public LessonProgressStorage(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public int GetLessonLastStep(int number)
        {
            string cookie;
            if (!httpContext.Request.Cookies.TryGetValue(ProgressCookieName, out cookie))
            {
                return 1;
            }

            var parts = cookie.Split(',');
            if (number - 1 < parts.Length)
            {
                return int.Parse(parts[number - 1]);
            }
            else
            {
                return 1;
            }
        }

        public void UpdateLessonLastStep(int number, int step)
        {
            string cookie;
            List<int> parts;
            if (!httpContext.Request.Cookies.TryGetValue(ProgressCookieName, out cookie))
            {
                parts = Enumerable.Range(0, number).Select(p => 1).ToList();
            }
            else
            {
                parts = cookie.Split(',').Select(int.Parse).ToList();
            }

            while (number - 1 < parts.Count)
            {
                parts.Add(1);
            }

            if (parts[number - 1] < step)
            {
                parts[number - 1] = step;
            }

            httpContext.Response.Cookies.Append(ProgressCookieName, string.Join(",", parts), new CookieOptions()
            {
                Expires = DateTime.Today.AddYears(1)
            });
        }

    }
}
