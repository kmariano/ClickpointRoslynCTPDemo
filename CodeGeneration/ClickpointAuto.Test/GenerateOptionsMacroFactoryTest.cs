using ClickpointAuto.Core.Factories;
using ClickPointAuto.Core.Factories;
using ClickpointAuto.Core.Models;
using ClickPointAuto.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickpointAuto.Test
{
    
    
    /// <summary>
    ///This is a test class for GenerateOptionsMacroFactoryTest and is intended
    ///to contain all GenerateOptionsMacroFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GenerateOptionsMacroFactoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CreateOptionsMacro
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Dev\\CodeSpace\\CodeGeneration\\ClickpointAuto.Web", "/")]
        //[UrlToTest("http://localhost:50483/")]
        public void CreateOptionsMacroTest()
        {

            ICarModel carModel = new CarModel
                                     {
                                         Id = 3,
                                         Color = "Platinum",
                                         SportsPackage = true,
                                         Price = 45000.99M,
                                         OptionsMacroText = "if(car.SportsPackage){ options.Add(\"6 inch Aero-Spoiler\"); }options.Add(\"Dual Exhaust\"); options.Add(\"Navigation Panel\");options.Add(\"Rear-Facing Camera\"); "

                                     };
            GenerateOptionsMacroFactory target = new GenerateOptionsMacroFactory(); // TODO: Initialize to an appropriate value
            var result = target.CreateOptionsMacro(carModel);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IGenerateOptionsMacro));
        }
    }
}
