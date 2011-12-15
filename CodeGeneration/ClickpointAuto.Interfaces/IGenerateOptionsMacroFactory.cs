using ClickpointAuto.Core.Models;


namespace ClickpointAuto.Core.Factories
{
    public interface IGenerateOptionsMacroFactory
    {
        IGenerateOptionsMacro CreateOptionsMacro(ICarModel car);
    }
}