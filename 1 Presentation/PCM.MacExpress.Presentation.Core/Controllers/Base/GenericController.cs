using PCM.MacExpress.Presentation.Core.Controllers.Security;
using PCM.MacExpress.Presentation.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Core.Controllers.Base
{
    [AuthorizeFilter]
    public class GenericController : Controller
    {
        public CuentaUsuario cuenta = HttpSessionContext.CurrentAccount();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpSessionContext.CurrentAccount() == null)
            {
                filterContext.Result = this.Redirect("~/");
                return;
            }
            if (!HttpSessionContext.ValidarAccesoUrl() && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = this.Redirect("~/LogOut/NotAuthorized");
                return;
            }
        }
    }
}
