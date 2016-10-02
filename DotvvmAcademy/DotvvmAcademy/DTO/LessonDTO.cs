using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DTO
{
    public class LessonDTO
    {

        public int Number { get; set; }

        public int LastStep { get; set; }

        public string Title { get; set; }

        public bool IsVisited => LastStep > 0;

    }
}
