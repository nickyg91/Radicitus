using System;
using System.Collections.Generic;
using System.Text;

namespace Radicitus.Entities
{
    public class RadGridNumber
    {
        public int RadNumberId { get; set; }
        public int GridId { get; set; }
        public int GridNumber { get; set; }
        public string RadMemberName { get; set; }
        public bool HasPaid { get; set; }
    }
}
