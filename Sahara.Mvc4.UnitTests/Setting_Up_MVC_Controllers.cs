using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Sahara.Mvc4;

namespace Sahara.Mvc.UnitTests
{
    [TestFixture]
    public class Setting_Up_MVC_Controllers
    {
        [Test]
        public void It_should_allow_access_to_the_Request()
        {
            var controllerSpec = new ControllerSpec<MonkeyController>();

            controllerSpec.Request.SetupGet(req => req.HttpMethod).Returns("GET");

            ((MonkeyController)controllerSpec).Request.HttpMethod.ShouldEqual("GET");

        }

        [Test]
        public void It_should_allow_access_to_the_Response()
        {
            var controllerSpec = new ControllerSpec<MonkeyController>();

            controllerSpec.Response.SetupGet(res => res.RedirectLocation).Returns("/home");

            ((MonkeyController)controllerSpec).Response.RedirectLocation.ShouldEqual("/home");

        }

        [Test]
        public void It_should_allow_access_to_the_User()
        {
            var controllerSpec = new ControllerSpec<MonkeyController>();
            controllerSpec.User.SetupGet(i => i.IsAuthenticated).Returns(true);

            var controller = controllerSpec.Build();

            (controller.Login() as ViewResult)
                .ShouldNotBeNull()
                .ViewName.ShouldEqual("LoggedIn");
        }

        [Test]
        public void It_should_allow_access_to_the_Session()
        {
            var controllerSpec = new ControllerSpec<MonkeyController>();
            controllerSpec.Session.SetupGet(session => session["Value"]).Returns("42");

            var controller = controllerSpec.Build();

            (controller.SessionAction() as ContentResult)
                .ShouldNotBeNull()
                .Content.ShouldEqual("42");
        }

        [Test]
        public void It_should_provide_access_to_injected_dependencies()
        {
            var controllerSpec = new ControllerSpec<MonkeyController>();
            controllerSpec.The<ISomeDependency>().Setup(d => d.LookupValue(It.IsAny<int>()))
                .Returns("Monkeys!!");

            var result = controllerSpec.Build().DependencyLookup(42) as ContentResult;

            result.ShouldNotBeNull()
                .Content.ShouldEqual("Monkeys!!");
        }
    }

    public class MonkeyController : Controller
    {
        private readonly ISomeDependency _someDependency;

        public MonkeyController(ISomeDependency someDependency)
        {
            _someDependency = someDependency;
        }

        public ActionResult Login()
        {
            return View(User.Identity.IsAuthenticated 
                ? "LoggedIn" 
                : "NotLoggedIn");
        }

        public ActionResult SessionAction()
        {
            return Content(Session["Value"].ToString());
        }

        public ActionResult DependencyLookup(int key)
        {
            return Content(_someDependency.LookupValue(key));
        }
    }

    public interface ISomeDependency
    {
        string LookupValue(int key);
    }

    public class SomeViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}