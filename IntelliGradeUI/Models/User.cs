namespace IntelliGradeUI.Models
{
    public class User
    {
        public string id { get; set; }

        public string nameSurname { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public List<string> teacherGrades { get; set; }

        public List<string> studentGrades { get; set; }
    }
}
