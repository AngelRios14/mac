using PCM.MacExpress.Presentation.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Core.Controllers.Security
{
    public class AuthorizeFilter : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (HttpSessionContext.CurrentAccount() == null)
            {
                filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl);
                filterContext.Result.ExecuteResult(filterContext);
            }
        }
    }
}
