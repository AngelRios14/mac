using System.Configuration;
using System.Web.Mvc;
using PCM.MacExpress.Presentation.Core.Session;
using System.Collections.Generic;
using System.Linq;
using System; 

namespace PCM.MacExpress.Presentation.Core.Controllers.Base
{
    public class PrincipalController : Controller// GenericController
    {
        #region Propiedades

        #endregion

        #region Vistas
        public ActionResult Index()
        {             
           /* if (cuenta != null)
            {
               
            }*/

            return View();
        }
        #endregion

        #region Vistas Parciales

        

        #endregion

        #region Json

        #endregion
    }
}
