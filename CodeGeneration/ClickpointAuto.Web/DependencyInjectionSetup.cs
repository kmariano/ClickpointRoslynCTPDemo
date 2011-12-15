
using ClickpointAuto.Core.Factories;
using ClickPointAuto.Core.Factories;
using StructureMap; 
namespace ClickpointAuto.Web
{
    public sealed class DependencyInjectionSetup
    {
         public static void SetupDependencyInjection()
         {
             ObjectFactory.Initialize(x =>
             {
                 x.Scan(

                     s =>
                     {
                         s.TheCallingAssembly();
                         s.WithDefaultConventions();
                     }
                     );
                 x.For<ICarModelFactory>().Use<CarModelFactory>();
                 x.For<IGenerateOptionsMacroFactory>().Use<GenerateOptionsMacroFactory>();
             });
         }
    }
}