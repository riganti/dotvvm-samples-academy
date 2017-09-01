using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson2ViewModel
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask()
        {
            //add the new task with the title set to AddedTaskTitle here

            //reset the AddedTaskTitle to an empty string
        }
    }
}