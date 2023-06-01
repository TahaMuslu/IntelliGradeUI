namespace IntelliGradeUI.Models
{
    public class Homework
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string filePath { get; set; }
        public string fileUrl { get; set; }
        public int? grade { get; set; }
        public string? description { get; set; }

    }
}
