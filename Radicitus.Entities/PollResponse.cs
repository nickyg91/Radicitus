

namespace Radicitus.Entities
{
    public class PollResponse
    {
        public int PollResponseId { get; set; }
        public int PollInputId { get; set; }
        public string Response { get; set; }
        public string Cookie { get; set; }
    }
}
