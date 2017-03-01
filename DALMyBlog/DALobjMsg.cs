using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DALMyBlog;
using ModelMyBlog;

namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:p_objmsg
    /// </summary>
    public partial class DALobjMsg
    {
        public DALobjMsg()
        { }
        #region  Method
        /// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int ID)
        //{
        //    int rowsAffected;
        //    MySqlParameter[] parameters = {
        //            new MySqlParameter("@ID", MySqlDbType.Int32)
        //    };
        //    parameters[0].Value = ID;

        //    int result = DbHelper.RunProcedure("p_objmsg_Exists", parameters, out rowsAffected);
        //    if (result == 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Add(objMsg model)
        {
            string str =
                "INSERT INTO p_objmsg(SpecificTypeID, Title, Msg, CreatDateTime, Remake) VALUE(@SpecificTypeID,@Title, @Msg, @CreatDateTime, @Remake);";
            MySqlParameter[] parameters = {
                  //  new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SpecificTypeID", MySqlDbType.Int32),
                    new MySqlParameter("@Title", MySqlDbType.VarChar,255),
                    new MySqlParameter("@Msg", MySqlDbType.VarChar,255),
                    new MySqlParameter("@CreatDateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@Remake", MySqlDbType.VarChar,255)};
          //  parameters[0].Value = model.ID;
            parameters[0].Value = model.SpecificTypeID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Msg;
            parameters[3].Value = model.CreatDateTime;
            parameters[4].Value = model.Remake;

            if (DBHelper.ExecuteSql(str, parameters)>=0)
            {
                return true;
            }
           return false;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(objMsg model)
        {
            string str =
                "UPDATE p_objmsg SET SpecificTypeID=@SpecificTypeID,Title=@Title,Msg=@Msg,CreatDateTime=@CreatDateTime,Remake=@Remake where ID=@ID;";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SpecificTypeID", MySqlDbType.Int32),
                    new MySqlParameter("@Title", MySqlDbType.VarChar,255),
                    new MySqlParameter("@Msg", MySqlDbType.VarChar,255),
                    new MySqlParameter("@CreatDateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@Remake", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.SpecificTypeID;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Msg;
            parameters[4].Value = model.CreatDateTime;
            parameters[5].Value = model.Remake;

            if (DBHelper.ExecuteSql(str, parameters) >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            string str = "DELETE FROM p_objmsg where ID=@ID;";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32)
            };
            parameters[0].Value = ID;

            if (DBHelper.ExecuteSql(str, parameters) >= 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from p_objmsg ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DBHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public objMsg GetModel(int ID)
        {
            string str = "SELECT * FROM p_objmsg WHERE ID=@ID; ";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32)
            };
            parameters[0].Value = ID;

            objMsg model = new objMsg();
            DataSet ds = DBHelper.Query(str, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public objMsg DataRowToModel(DataRow row)
        {
            objMsg model = new objMsg();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SpecificTypeID"] != null)
                {
                    model.SpecificTypeID =int.Parse(row["SpecificTypeID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Msg"] != null)
                {
                    model.Msg = row["Msg"].ToString();
                }
                if (row["CreatDateTime"] != null && row["CreatDateTime"].ToString() != "")
                {
                    model.CreatDateTime = DateTime.Parse(row["CreatDateTime"].ToString());
                }
                if (row["Remake"] != null)
                {
                    model.Remake = row["Remake"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SpecificTypeID,Title,Msg,CreatDateTime,Remake ");
            strSql.Append(" FROM p_objmsg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM p_objmsg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DBHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from p_objmsg T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DBHelper.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "p_objmsg";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

