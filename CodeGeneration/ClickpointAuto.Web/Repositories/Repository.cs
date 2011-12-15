using System;
using System.Collections.Generic;
using System.Data.Objects;
using ClickpointAuto.Web.Models;
using System.Linq;

namespace ClickpointAuto.Web.Repositories
{
    public class CarRepository : ICarRepository
    {
        private ClickPointAutoContext _context = null; 
        public  CarRepository()
        {
            _context = new ClickPointAutoContext();
        }

        public ObjectContext Context { get { return _context; } }

        public Car FindCar(int id)
        {
            var car = _context.Cars.Where(c => c.Id == id).FirstOrDefault();
            return car; 
        }

        public void AddCar(Car car)
        {
            _context.AddToCars(car);
        }
        #region Implementation of IDisposable

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}