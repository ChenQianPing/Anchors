using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using wfEnginer;

namespace Anchors
{
    class TableInfo
    {
        private static TableInfo mInstance=null;
        private DataTable tbInfo = null;
        private DataTable colInfo = null;
        
        public static TableInfo GetInstance()
        {

            if (mInstance == null)
                mInstance = new TableInfo();
            return mInstance;
        }

        public DataTable TableInfoSet
        {
            get
            {
                if (tbInfo == null)
                {
                    IDataReader dr = dbData.getDataReader(dbConn.DBConn,
                        "select  F_ID, F_NAME, F_DISPLAY, F_DESC from  WF_TABLES");


                    tbInfo = new DataTable();
                    FillDataTable(dr, tbInfo);

                    dr.Dispose();
                }

                return tbInfo;
            }
        }

        public DataTable ColumnInfoSet
        {
            get
            {
                if (colInfo == null)
                {
                    IDataReader dr = dbData.getDataReader(dbConn.DBConn,
                        "SELECT A.F_ID, A.F_TABLE_ID, A.F_NAME, A.F_DISPLAY, A.F_DATA_TYPE, A.F_IS_KEY,B.F_DISPLAY as F_TABLENAME "+
                        " FROM WF_TABLE_COLUMN A,WF_TABLES B where A.F_TABLE_ID=B.F_ID");


                    colInfo = new DataTable();
                    FillDataTable(dr, colInfo);

                    dr.Dispose();
                }

                return colInfo;
            }
        }

        private void FillDataTable(IDataReader dr, DataTable dt)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {

                DataColumn col = new DataColumn();
                col.ColumnName = dr.GetName(i);
                col.DataType = dr.GetFieldType(i);

                dt.Columns.Add(col);
            }

            while (dr.Read())
            {

                DataRow row = dt.NewRow();
                for (int i = 0; i < dr.FieldCount; i++)
                    row[i] = dr[i];

                dt.Rows.Add(row);
            }
        }
    }
}
