
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Radicitus.Web.Models
{
    public class GridModel
    {
        [Required(ErrorMessage = "A Grid Name is required.")]
        public string GridName { get; set; }
        [Required(ErrorMessage = "A Cost Per Square is required."),
         RegularExpression("\\d+", ErrorMessage = "Cost must be a number.")]
        public int CostPerSquare { get; set; }
    }
}
