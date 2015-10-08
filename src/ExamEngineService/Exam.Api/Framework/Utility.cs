using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Exam.Api.Framework
{
    public class Utility
    {
        /**/
        /// <summary>
        /// 返回Excel数据源
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="TSql">TSql</param>
        /// <returns>DataSet</returns>
        public static DataSet ExcelToDataSet(string filename, string tSql)
        {
            DataSet ds;
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + filename;
            OleDbConnection myConn = new OleDbConnection(strCon);
            string strCom = tSql;
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            ds = new DataSet();
            myCommand.Fill(ds);
            myConn.Close();
            return ds;
        }
    }
}