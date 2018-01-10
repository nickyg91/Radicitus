using System.Collections.Generic;

namespace Radicitus.Web.Models
{
    public class AddMembersModel
    {
        public List<RadMemberModel> Members { get; set; }
        public int GridId;
    }
}
