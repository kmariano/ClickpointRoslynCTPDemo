using System.IO;
using System.Reflection;
using ClickpointAuto.Core.Factories;
using ClickpointAuto.Core.Models;
using ClickPointAuto.Core.Models;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace ClickPointAuto.Core.Factories
{
    public class GenerateOptionsMacroFactory : IGenerateOptionsMacroFactory
    {
        public IGenerateOptionsMacro CreateOptionsMacro(ICarModel car)
        {
            IGenerateOptionsMacro optionsMacro = null;
            var codeFile = @"using System; 
                             using System.Collections.Generic;
                             using ClickpointAuto.Core.Factories;
                             namespace ClickpointAuto.Core.Models
                             {
                                public class GenerateOptionsMacro : IGenerateOptionsMacro
                                {
                                   public List<string> GenerateOptions(ICarModel car)
                                   {
                                      var options = new List<string>();
                                      $
                                      return options;
                                   }   
                                }
                            }";
            codeFile = codeFile.Replace("$", car.OptionsMacroText);
            var syntaxTree = SyntaxTree.ParseCompilationUnit(codeFile);
            var compilation = Compilation.Create("ClickPointAuto.GenerateOptions", 

                                                  options: new CompilationOptions(
                                                      assemblyKind: AssemblyKind.DynamicallyLinkedLibrary))
                .AddReferences(new MetadataReference[]
                                   {
                                       new AssemblyFileReference(typeof (object).Assembly.Location),
                                       new AssemblyFileReference(typeof (IGenerateOptionsMacro).Assembly.Location),
                                       new AssemblyFileReference(typeof(CarModel).Assembly.Location)
                                       
                                   }).AddSyntaxTrees(syntaxTree);

            Assembly compiledAssembly; 
            using (var stream = new MemoryStream())
            {
                compilation.Emit(stream);
                compiledAssembly = Assembly.Load(stream.GetBuffer());
            }
            optionsMacro = compiledAssembly.
                               CreateInstance("ClickpointAuto.Core.Models.GenerateOptionsMacro", false) as IGenerateOptionsMacro;
            return optionsMacro; 
        }
    }
}