using System.Collections.Generic;
using Radicitus.Entities;

namespace Radicitus.Web.Models
{
    public class PollModel
    {
        public Poll Poll { get; set; }
        public IEnumerable<PollInput> PollInputs { get; set; }
    }
}
