using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class CreatePlayerViewModel
    {
        [Required]
        [Display(Name = "Player Name")]
        [StringLength(128, ErrorMessage = "Players name can only be 128 characters in length.")]
        public string Name { get; set; }

        public List<string> TeamIds { get; set; }

        [Display(Name = "Teams")]
        public MultiSelectList Teams { get; set; }

        public int[] SelectedValues { get; set; }
    }
}
