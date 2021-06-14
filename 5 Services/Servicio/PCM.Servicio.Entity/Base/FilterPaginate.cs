using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity.Base
{
    public class FilterPaginate
    {

        private int PageNumber;

        private int PageRows;
        private String order;
        private String Connection;


        public String getConnection()
        {
            return Connection;
        }

        public void setConnection(String Connection)
        {
            this.Connection = Connection;
        }

        public String getOrder()
        {
            return order;
        }

        public void setOrder(String order)
        {
            this.order = order;
        }

        public int getFromPage()//desde
        {
            return (PageNumber * PageRows) + 1;
        }

        public int getUpPage()//hasta
        {
            return (PageNumber + 1) * PageRows;
        }


        public FilterPaginate()
        {
            this.PageNumber = 1;
            this.PageRows = int.MaxValue;
        }

        public int getPageNumber()
        {
            return PageNumber;
        }

        public void setPageNumber(int PageNumber)
        {
            this.PageNumber = PageNumber;
        }

        public int getPageRows()
        {
            return PageRows;
        }

        public void setPageRows(int PageRows)
        {
            this.PageRows = PageRows;
        }

    }
}
