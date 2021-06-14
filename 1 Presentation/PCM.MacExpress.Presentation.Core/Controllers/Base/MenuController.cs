using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCM.MacExpress.Presentation.Core.ViewModel;


namespace PCM.MacExpress.Presentation.Core.Controllers.Base
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

            var menu = new List<NavbarModels>();
            if (oBEUsuario != null)
                foreach (var menuItemResponse in oBEUsuario.Lista_Recurso)
                {
                    var objmenu = new NavbarModels();
                    objmenu.Id = menuItemResponse.ID_RECURSO;
                    objmenu.nameOption = menuItemResponse.TITULO;
                    objmenu.controller = menuItemResponse.PAGINA;
                    objmenu.action = menuItemResponse.URLMENU;
                    objmenu.nombreUsuario = string.Concat(oBEUsuario.NOMBRE, " ", oBEUsuario.APELLIDO_PATERNO);
                    objmenu.area = menuItemResponse.AREA;

                    objmenu.status = true;

                    if (menuItemResponse.ID_RECURSO_PARENT == 0)
                    {
                        switch (objmenu.Id)
                        {   case 1: objmenu.imageClass= "fas fa-cog  icono-menu"; break;
                            case 2: objmenu.imageClass = "fas fa-clipboard-list  icono-menu"; break;
                            case 8: objmenu.imageClass = "fas fa-list-alt  icono-menu"; break;                            
                            default: objmenu.imageClass = "fa fa-sitemap fa-fw  icono-menu"; break; // es una Imagen Fija por prueba cambiar luego
                        }
                        objmenu.isParent = true;
                        objmenu.parentId = 0;
                    }
                    else
                    {
                        objmenu.isParent = false;
                        objmenu.parentId = menuItemResponse.ID_RECURSO_PARENT;
                    }
                    menu.Add(objmenu);
                }

            return PartialView("Index", menu.ToList());
        }

        public ActionResult IndexCabecera()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            var objmenu = new GeneralModels();

            if (oBEUsuario != null) {
                objmenu.nombreUsuario = string.Concat(oBEUsuario.NOMBRE, " ", oBEUsuario.APELLIDO_PATERNO, " ", oBEUsuario.APELLIDO_MATERNO);
                objmenu.nombreRol = oBEUsuario.Lista_Permiso[oBEUsuario.Lista_Permiso.Count - 1].ROL;
                objmenu.nombreEntidad = oBEUsuario.NOMBRE_ENTIDAD;
            }

            return PartialView("IndexCabecera", objmenu);
        }        
    }
}
