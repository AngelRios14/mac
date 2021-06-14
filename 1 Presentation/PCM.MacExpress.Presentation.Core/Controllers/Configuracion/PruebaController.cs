using Newtonsoft.Json;
using PCM.MacExpress.Presentation.Core.Infraestructura;
using PCM.MacExpress.Presentation.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Areas.Configuracion
{
    public class PruebaController : WebBaseController
    {
        // GET: Configuracion/Prueba
        public ActionResult Index()
        {
            //List<Team> teams = new List<Team>();
            ////MultiSelectList teamsList = new MultiSelectList(db.Teams.ToList().OrderBy(i => i.Name), "TeamId", "Name");

            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 01" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 02" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 03" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 04" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 05" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 06" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 07" });
            //teams.Add(new Team() { TeamId = new Guid(), Name = "Name 08" });

            //List<SelectListItem> items = new List<SelectListItem>();

            //foreach (var team in teams)
            //{
            //    var item = new SelectListItem
            //    {
            //        Value = team.TeamId.ToString(),
            //        Text = team.Name
            //    };

            //    items.Add(item);
            //}


            //MultiSelectList teamsList = new MultiSelectList(items.OrderBy(i => i.Text), "Value", "Text");
            ////var playerTeams = teams.Where(t => t.Players.Contains(player)).ToList();

            //ViewBag.LST_ENTIDAD_SERVICIO = FnListarEntidadServicio();

            //CreatePlayerViewModel model = new CreatePlayerViewModel { Teams = teamsList };
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            EntidadModels model = new EntidadModels();

            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", string.Empty,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", "false",
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<EntidadModels>>>(strRespuesta);

            model.GetEntidadList = ObjetoJSON.Data.Result;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EntidadModels model)
        {
            //if (ModelState.IsValid)
            //{
            //    Player player = new Player
            //    {
            //        PlayerId = Guid.NewGuid(),
            //        Name = model.Name
            //    };

            //}
            return View();
        }

            // GET: Configuracion/Prueba/Details/5
            public ActionResult Details(int id)
        {
            

            return View();
        }

        // GET: Configuracion/Prueba/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuracion/Prueba/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuracion/Prueba/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuracion/Prueba/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuracion/Prueba/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuracion/Prueba/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public SelectList FnListarEntidadServicio()
        {
            string COD_UBIGEO = string.Empty;

            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", string.Empty,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", "false",
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return new SelectList(ObjetoJSON.Data.Result, "ID_ENTIDAD", "NOMBRE");
            else
            {
                List<BEEntidad> data_ = new List<BEEntidad>();
                data_.Add(new BEEntidad { ID_ENTIDAD = 0, NOMBRE = "SELECCIONE" });
                return new SelectList(data_, "ID_ENTIDAD", "NOMBRE");
            }
        }
    }
}
