using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Radicitus.Web.Models
{
    public class RadMemberModel
    {
        [Required(ErrorMessage = "Member Name cannot be empty.")]
        public string MemberName { get; set; }
        [RegularExpression("^((\\d+)(,\\d+){4})$", 
            ErrorMessage = "The box must contain a comma separated list of numbers.")]
        public string NumberCsv { get; set; }
        public List<int> GridNumbers => NumberCsv.Split(",").Select(int.Parse).ToList();
    }
}
