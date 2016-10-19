using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace CS.Data
{
    /// <summary>
    /// DataTable与实体类互相转换
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public class ModelHandler<T> where T : new()
    {
        #region DataTable转换成实体类

        /// <summary>
        /// 填充对象列表：用DataSet的第一个表填充实体类
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public List<T> FillModel(DataSet ds)
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return FillModel(ds.Tables[0]);
        }

        /// <summary>  
        /// 填充对象列表：用DataSet的第index个表填充实体类
        /// </summary>  
        public List<T> FillModel(DataSet ds, int index)
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            return FillModel(ds.Tables[index]);
        }

        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public List<T> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            var modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //T model = (T)Activator.CreateInstance(typeof(T));  
                var model = new T();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    //dr里的值  dr[propertyInfo.Name]   ;  属性名称  propertyInfo.Name
                    // model.GetType().GetProperty(propertyInfo.Name).GetType()
                    model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name] == DBNull.Value ? string.Empty : dr[propertyInfo.Name],null);
                }
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>  
        /// 填充对象：用DataRow填充实体类
        /// </summary>  
        public T FillModel(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }
            //T model = (T)Activator.CreateInstance(typeof(T));  
            var model = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
            }
            return model;
        }

        #endregion

        #region 实体类转换成DataTable

        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataSet FillDataSet(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            var ds = new DataSet();
            ds.Tables.Add(FillDataTable(modelList));
            return ds;
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataTable FillDataTable(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            var dt = CreateDataTable();

            foreach (T model in modelList)
            {
                var dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 根据实体类得到表结构及期实例
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        #endregion
    } 
}