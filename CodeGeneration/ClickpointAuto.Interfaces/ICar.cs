using System;

namespace ClickpointAuto.Web.Models
{
    public interface ICar
    {
        int Id { get; set; }
        string Model { get; set; }
        string Color { get; set; }
        decimal? Price { get; set; }
        string OptionsMacroText { get; set; }
        bool? SportsPackage { get; set; }
    }
}