using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Assignment
    {
        public string id { get; set; }
        public string teacherId { get; set; }
        public string filePath { get; set; }
        public string fileUrl { get; set; }
        public string title { get; set; }
        public string definition { get; set; }
        public string description { get; set; }
        public List<string> requirements { get; set; }
        public List<Homework> uploadedHomeworks { get; set; }
        public DateTime date { get; set; }
        public DateTime deadline { get; set; }
    }
}
