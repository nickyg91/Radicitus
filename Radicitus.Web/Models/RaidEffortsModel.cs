using System.Collections.Generic;
using Radicitus.Entities;

namespace Radicitus.Web.Models
{
    public class RaidEffortsModel
    {
        public IEnumerable<Grid> Grids { get; set; }
        public int TotalGoldDonated { get; set; }
        public Dictionary<int, int> TotalGoldPerGrid { get; set; }
    }
}
