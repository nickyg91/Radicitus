using System;
using System.Collections.Generic;
using System.Text;

namespace Radicitus.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
