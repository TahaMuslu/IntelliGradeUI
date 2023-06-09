﻿namespace IntelliGradeUI.Models
{
    public class Lesson
    {
        public string id { get; set; }
        public string className { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string classCode { get; set; }
        public List<string> teacherIds { get; set; }
        public List<string> studentIds { get; set; }
        public List<string> assignmentIds { get; set; }
        public List<string> quizIds { get; set; }
  
    }
}
