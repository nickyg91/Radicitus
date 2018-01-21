using System.Collections.Generic;
using Radicitus.Entities;

namespace Radicitus.Web.Models
{
    public class ViewGridModel
    {
        public Grid Grid { get; set; }
        public Dictionary<int, RadGridNumber> MemberNumbers { get; set; }
        public HashSet<int> UsedNumbers { get; set; }
    }
}
