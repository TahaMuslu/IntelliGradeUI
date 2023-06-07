using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Quiz
    {
        public string id { get; set; }
        public string teacherId { get; set; }
        public string classId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<Question> questions { get; set; }
        public string topics { get; set; }
        public int questionCount { get; set; }
        public List<Entrance> Entrances { get; set; }

    }
}
