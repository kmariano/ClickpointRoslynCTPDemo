using System.Collections.Generic;
using System.Linq;
using ClickpointAuto.Core.Factories;
using ClickpointAuto.Core.Models;
using ClickPointAuto.Core.Models;
using ClickpointAuto.Web.Models;

namespace ClickPointAuto.Core.Factories
{
    public class CarModelFactory : ICarModelFactory
    {

        public IEnumerable<ICarModel> GetCarModels(IEnumerable<ICar> cars)
        {
            var carModels = from c in cars
            select new CarModel
                       {

                           Id = c.Id,
                           Color = c.Color,
                           Price = c.Price.HasValue ? c.Price.Value : 0.00M,
                           OptionsMacroText = c.OptionsMacroText,
                           VehicleModel = c.Model,
                           SportsPackage = c.SportsPackage.HasValue ? c.SportsPackage.Value : false

                       };
            return (IEnumerable<ICarModel>) carModels.ToList(); 

        }

        public ICarModel GetCarModel(ICar car)
        {
            var carModel = new CarModel
                               {
                                   Color = car.Color,

                                   Id = car.Id,
                                   OptionsMacroText = car.OptionsMacroText,
                                   Price = car.Price.HasValue ? car.Price.Value : 0.00M,
                                   SportsPackage = car.SportsPackage.HasValue ? car.SportsPackage.Value : false,
                                   VehicleModel = car.Model

                               };
            return (ICarModel) carModel;
        }
    }
}