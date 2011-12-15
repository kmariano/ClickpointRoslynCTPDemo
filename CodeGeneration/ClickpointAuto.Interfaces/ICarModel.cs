using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickpointAuto.Core.Models
{
    public interface ICarModel
    {
        int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please Select a Model")]
        string VehicleModel { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please Select a Color")]
        string Color { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        decimal Price { get; set; }

        [Display(Name = "Options List Macro")]
        string OptionsMacroText { get; set; }

        [Display(Name = "Sports Package")]
        bool SportsPackage { get; set; }

        List<string> ConfigurationOptions { get; }
    }
}