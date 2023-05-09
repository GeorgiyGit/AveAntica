using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Message:Monitoring
    {
        public int Id { get; set; }
        public Customer Owner { get; set; }
        public string OwnerId { get; set; }

        public string Text { get; set; }
        public bool IsReaded { get; set; }

        public GlobalChat? GlobalChat { get; set; }
        public int? GlobalChatId { get; set; }

        public LocalChat? LocalChat { get; set; }
        public int? LocalChatId { get; set; }
    }
}
