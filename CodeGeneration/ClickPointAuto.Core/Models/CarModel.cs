using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClickpointAuto.Core.Models;

namespace ClickPointAuto.Core.Models
{
    public class CarModel : ICarModel
    {
        private List<string> _configurationOptions = new List<string>();
        public int Id { get; set;  }
        
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please Select a Model")]
        public string VehicleModel { get; set;  }
        
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please Select a Color")]
        public string Color { get; set;  }

        [Required(ErrorMessage =  "Please enter a Price")]
        public decimal Price { get; set;  }

        [Display(Name="Options List Macro")]
        public string OptionsMacroText { get; set;  }

        
        [Display(Name="Sports Package")]
        public bool SportsPackage { get; set;  }
        public List<string> ConfigurationOptions { get { return _configurationOptions; }}

    }
}