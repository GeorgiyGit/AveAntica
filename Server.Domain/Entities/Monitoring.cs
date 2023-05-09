using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public interface IMonitoring
    {
        public DateTime CreationTime { get; set; }
        
        public bool IsEdited { get; set; }
        public DateTime LastEditTime { get; set; }
        
        public bool IsDeleted { get; set; }
        public DateTime LastDeleteTime { get; set; }
    }
    public abstract class Monitoring : IMonitoring
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public bool IsEdited { get; set; }
        public DateTime LastEditTime { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime LastDeleteTime { get; set; }
    }
}
