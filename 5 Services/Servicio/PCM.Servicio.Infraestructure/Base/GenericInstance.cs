using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace PCM.Servicio.Infraestructure.Base
{
    public class GenericInstance<T>
    {
        public T readDataReader(IDataReader oIDataReader)
        {
            if (oIDataReader.Read())
            {
                var Object = Activator.CreateInstance<T>();
                Type oType = Object.GetType();
                foreach (PropertyInfo oPropertyInfo in oType.GetProperties())
                {
                    if (this.ColumnExists(oIDataReader, oPropertyInfo.Name))
                    {
                        oPropertyInfo.SetValue(Object, DefaultValue(oPropertyInfo, oIDataReader[oPropertyInfo.Name]));
                    }
                }
                return Object;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oIDataReader"></param>
        /// <returns></returns>
        public List<T> readDataReaderList(IDataReader oIDataReader)
        {
            Type oType = typeof(T);
            List<T> lstObject = new List<T>();
            while (oIDataReader.Read())
            {
                var Object = Activator.CreateInstance<T>();
                foreach (PropertyInfo oPropertyInfo in oType.GetProperties())
                {
                    if (this.ColumnExists(oIDataReader, oPropertyInfo.Name))
                    {
                        oPropertyInfo.SetValue(Object, DefaultValue(oPropertyInfo, oIDataReader[oPropertyInfo.Name]));
                    }
                }
                lstObject.Add(Object);
            }
            
            
            return lstObject;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oIDataReader"></param>
        /// <returns></returns>
        public List<T> readDataTableList(DataTable oIDataReader)
        {
            Type oType = typeof(T);
            List<T> lstObject = new List<T>();
            //while (oIDataReader.Read())
            //{
            //    var Object = Activator.CreateInstance<T>();
            //    foreach (PropertyInfo oPropertyInfo in oType.GetProperties())
            //    {
            //        if (this.ColumnExists(oIDataReader, oPropertyInfo.Name))
            //        {
            //            oPropertyInfo.SetValue(Object, DefaultValue(oPropertyInfo, oIDataReader[oPropertyInfo.Name]));
            //        }
            //    }
            //    lstObject.Add(Object);
            //}
            return lstObject;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oIDataReader"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader oIDataReader, string sColumnName)
        {
            for (int i = 0; i < oIDataReader.FieldCount; i++)
            {
                if (oIDataReader.GetName(i).Equals(sColumnName))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPropertyInfo"></param>
        /// <param name="objet"></param>
        /// <returns></returns>
        private static object DefaultValue(PropertyInfo oPropertyInfo, object objet)
        {
            if (Convert.IsDBNull(objet))
            {
                return null;
            }
            else
            {
                return objet;
            }
        }
    }
}
