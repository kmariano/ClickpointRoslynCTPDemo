
using System.Collections.Generic;
using System.Web.Mvc;
using ClickpointAuto.Core.Factories;
using ClickpointAuto.Core.Models;
using ClickPointAuto.Core.Models;
using ClickpointAuto.Web.Models;
using ClickpointAuto.Web.Repositories;
using StructureMap;

namespace ClickpointAuto.Web.Controllers
{
    public class CarController : Controller
    {
  
        private ICarRepository _repository = null;
        private ICarModelFactory _carModelFactory = null;
        private IGenerateOptionsMacroFactory _generateOptionsMacroFactory = null; 
        public ICarRepository Repository
        {
            get { return _repository ?? (_repository = ObjectFactory.GetInstance<ICarRepository>()); }
        }
        public ICarModelFactory CarModelFactory
        {
            get { return _carModelFactory ?? (_carModelFactory = ObjectFactory.GetInstance<ICarModelFactory>()); }
        }
        public IGenerateOptionsMacroFactory GenerateOptionsMacroFactory
        {
            get { return _generateOptionsMacroFactory ?? (_generateOptionsMacroFactory = ObjectFactory.GetInstance<IGenerateOptionsMacroFactory>());}
        }
        //

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public ActionResult ShowOptions(int id)
        {
            var carModel = CarModelFactory.GetCarModel(Repository.FindCar(id));
            List<string> options = null;
            if (carModel != null)
            {
                IGenerateOptionsMacro macro = GenerateOptionsMacroFactory.CreateOptionsMacro(carModel);
                options = macro.GenerateOptions(carModel);
            }
            return Json(options, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Car/
        public ActionResult Index()
        {
            var model = CarModelFactory.GetCarModels(Repository.GetAllCars());
            return View("Index", model);
        }

        //
        // GET: /Car/Details/5

        public ActionResult Details(int id)
        {
            var carModel = CarModelFactory.GetCarModel(Repository.FindCar(id)); 
            return View("Details", carModel);
        }

        //
        // GET: /Car/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetDropDownLists();
            return View("Create");
        } 


        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(CarModel model)
        {

            if(ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    var newCar = new Car
                                     {
                                         Color = model.Color,
                                         OptionsMacroText = model.OptionsMacroText,
                                         Model = model.VehicleModel,
                                         Price = model.Price,
                                         SportsPackage = model.SportsPackage
                                     };

                    Repository.AddCar(newCar);
                    Repository.Context.SaveChanges();
                    model.Id = newCar.Id; 
                    return View("Details", model); 
                }
            }
            SetDropDownLists();
            return View("Create");
        }
        

        private static IEnumerable<SelectListItem> GetColors()
        {
            var colorItems = new List<SelectListItem>();
            colorItems.Add(new SelectListItem
                               {
                                   Text = "Cobalt Blue",
                                   Value = "CobaltBlue"

                               });
            colorItems.Add(new SelectListItem
            {
                Text = "Platinum",
                Value = "Platinum"

            });
            colorItems.Add(new SelectListItem
            {
                Text = "Midnight Blue",
                Value = "MidnightBlue"

            });
            colorItems.Add(new SelectListItem
            {
                Text = "Ivory",
                Value = "Ivory"

            });
            return colorItems;
        }

        private static IEnumerable<SelectListItem> GetModels()
        {
            var modelItems = new List<SelectListItem>();
            modelItems.Add(new SelectListItem
            {
                Text = "The Sedan",
                Value = "Sedan"

            });
            modelItems.Add(new SelectListItem
            {
                Text = "Racer",
                Value = "Racer"

            });
            modelItems.Add(new SelectListItem
            {
                Text = "Sport Utility Vehicle",
                Value = "SportUtility"

            });
            return modelItems;
        }


        private void SetDropDownLists()
        {
            ViewBag.Models = GetModels();
            ViewBag.Colors = GetColors();
        }
    }
}
