using System.Collections.Generic;
using ClickpointAuto.Core.Models;
using ClickpointAuto.Web.Models;

namespace ClickpointAuto.Core.Factories
{
    public interface ICarModelFactory
    {
        IEnumerable<ICarModel> GetCarModels(IEnumerable<ICar> cars);
        ICarModel GetCarModel(ICar car);
    }
}