using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Invite
    {
        public string id { get; set; }

        public string classId { get; set; }
        public string senderId { get; set; }    
        public string receiverId { get; set; }

        public string className { get; set; }

        public string senderName { get; set; }

    }
}
