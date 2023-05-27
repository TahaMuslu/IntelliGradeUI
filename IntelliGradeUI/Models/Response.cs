namespace IntelliGradeUI.Models
{
    public class Response
    {
        public string message { get; set; }
        public string status { get; set; }

        public List<Lesson> lessons { get; set; }

        public Response(string message, string status)
        {
            this.message = message;
            this.status = status;
        }

        public Response (List<Lesson> lessons,string status)
        {
            this.lessons = lessons;
            this.status = status;
        }

        public Response()
        {
        }
    }
}
