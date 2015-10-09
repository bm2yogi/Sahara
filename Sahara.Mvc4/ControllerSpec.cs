using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Sahara.Mvc4
{
    /// <summary>
    ///     Builds a class of type T, creating and exposing mocked instances
    ///     of any dependent classes declared as interfaces in the class' constructor method.
    /// </summary>
    /// <typeparam name="T">The type of the class to be created. Must be a class.</typeparam>
    public class ControllerSpec<T> : Spec<T> where T : class
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public ControllerSpec()
        {
            MockDictionary = new[]
            {
                typeof (HttpRequestBase),
                typeof (HttpResponseBase),
                typeof (HttpSessionStateBase),
                typeof (HttpContextBase),
                typeof (RouteData),
                typeof (IIdentity),
                typeof (IPrincipal)
            }
                .ToDictionary(t => t, BuildMockObject);
        }

        /// <summary>
        ///     Provides access to a mocked instance of the Request object.
        /// </summary>
        public Mock<HttpRequestBase> Request
        {
            get { return The<HttpRequestBase>(); }
        }

        /// <summary>
        ///     Provides access to a mocked instance of the Response object.
        /// </summary>
        public Mock<HttpResponseBase> Response
        {
            get { return The<HttpResponseBase>(); }
        }

        /// <summary>
        ///     Provides access to a mocked instance of the Session object.
        /// </summary>
        public Mock<HttpSessionStateBase> Session
        {
            get { return The<HttpSessionStateBase>(); }
        }

        /// <summary>
        ///     Provides access to a mocked instance of the HttpContext object
        /// </summary>
        public Mock<HttpContextBase> HttpContext
        {
            get { return The<HttpContextBase>(); }
        }

        /// <summary>
        ///     Provides access to a mocked instance of the RouteData object.
        /// </summary>
        public Mock<RouteData> RouteData
        {
            get { return The<RouteData>(); }
        }

        /// <summary>
        ///     Provides access to a mocked instance of the User object.
        /// </summary>
        public Mock<IIdentity> User
        {
            get { return The<IIdentity>(); }
        }

        /// <summary>
        ///     Provides access to the class' dependencies (as declared
        ///     in the constructor(s)) as mock objects. Allows the setting of
        ///     expectations and behaviors on those mocks.
        /// </summary>
        /// <typeparam name="TI">The interface type of the requested dependency.</typeparam>
        /// <returns>
        ///     The mocked instance that was a declared dependency of the target class.
        ///     Returns null if there is no dependency that implements <see cref="TI" />.
        /// </returns>
        public override Mock<TI> The<TI>()
        {
            var t = typeof(TI);
            if (!MockDictionary.ContainsKey(t))
            {
                MockDictionary[t] = BuildMockObject(t);
            }

            return MockDictionary[t] as Mock<TI>;
        }

        /// <summary>
        ///     Invokes the constructor of the target class, passing in
        ///     the class' dependencies as mocked instances and returns
        ///     the target class.
        /// </summary>
        /// <returns>The target class (of type T).</returns>
        public override T Build()
        {
            return SetupControllerContext(base.Build());
        }

        private T SetupControllerContext(T sut)
        {
            var controller = sut as Controller;

            Mock<IIdentity> identity = The<IIdentity>();
            Mock<IPrincipal> principal = The<IPrincipal>();

            principal
                .SetupGet(p => p.Identity)
                .Returns(identity.Object);

            Mock<HttpContextBase> httpContextBase = The<HttpContextBase>();
            Mock<HttpSessionStateBase> httpSessionStateBase = The<HttpSessionStateBase>();
            Mock<HttpRequestBase> httpRequestBase = The<HttpRequestBase>();
            Mock<HttpResponseBase> httpResponseBase = The<HttpResponseBase>();
            Mock<RouteData> routeData = The<RouteData>();

            httpContextBase
                .SetupGet(c => c.Request)
                .Returns(httpRequestBase.Object);

            httpContextBase
                .SetupGet(c => c.Response)
                .Returns(httpResponseBase.Object);

            httpContextBase
                .SetupGet(c => c.User).Returns(principal.Object);

            httpContextBase
                .SetupGet(c => c.Session).Returns(httpSessionStateBase.Object);

            controller.ControllerContext =
                new ControllerContext(httpContextBase.Object, routeData.Object, controller);

            return controller as T;
        }

        /// <summary>
        ///     Performs an implicit type conversion from ControllerSpec&lt;T&gt; to a class of type T.
        /// </summary>
        /// <param name="spec">A class of type ControllerSpec&lt;T&gt;.</param>
        /// <returns>A class of type T.</returns>
        public static implicit operator T(ControllerSpec<T> spec)
        {
            return spec.Build();
        }
    }
}