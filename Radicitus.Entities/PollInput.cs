
namespace Radicitus.Entities
{
    public class PollInput
    {
        public int PollInputId { get; set; }
        public PollInputType PollInputTypeId { get; set; }
        public int PollId { get; set; }
        public string Answer { get; set; }
    }
}
