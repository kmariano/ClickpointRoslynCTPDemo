using System;
using System.Collections.Generic;
using System.Data.Objects;
using ClickpointAuto.Web.Models;

namespace ClickpointAuto.Web.Repositories
{
    public interface ICarRepository : IDisposable
    {
        Car FindCar(int id);
        List<Car> GetAllCars();
        void AddCar(Car car);
        ObjectContext Context { get; }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        void Dispose();
    }
}