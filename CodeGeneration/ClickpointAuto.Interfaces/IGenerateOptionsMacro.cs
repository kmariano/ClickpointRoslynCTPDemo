using System.Collections.Generic;


namespace ClickpointAuto.Core.Models
{
    public interface IGenerateOptionsMacro
    {
        List<string> GenerateOptions(ICarModel car);
    }
}