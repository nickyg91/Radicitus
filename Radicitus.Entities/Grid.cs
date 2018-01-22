using System;

namespace Radicitus.Entities
{
    public class Grid
    {
        public int GridId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CostPerSquare { get; set; }
        public string GridName { get; set; }
    }
}
