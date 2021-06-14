using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BERecurso
    {
        public int ID_RECURSO { get; set; }

        public int ID_RECURSO_PARENT { get; set; }

        public int ORDEN { get; set; }

        public string TITULO { get; set; }

        public string PAGINA { get; set; }

        public string IMAGEN { get; set; }

        public string URLMENU { get; set; }

        public string AREA { get; set; }
        public int RECURSO_DEFECTO { get; set; }
    }
}
