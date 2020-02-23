namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represents base HVB MVC controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public abstract class BaseController : Controller
    {
        private NLog.Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public NLog.Logger Logger 
        { 
            get { return this._logger; }
        }
    }
}
