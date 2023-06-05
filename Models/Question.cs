using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Question
    {
        public string id { get; set; }
        public string questionText { get; set; }
        public string correctAnswer { get; set; }
        public List<string> answers { get; set; }
    }
}
