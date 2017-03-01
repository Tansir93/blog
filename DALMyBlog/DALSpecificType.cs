using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelMyBlog;
using MySql.Data.MySqlClient;

namespace DALMyBlog
{
    /// <summary>
    /// 数据访问类:p_objspecifictype
    /// </summary>
    public partial class DALSpecificType
    {
        public DALSpecificType()
        { }
        #region  Method
        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int ID)
        //{
        //    int rowsAffected;
        //    MySqlParameter[] parameters = {
        //            new MySqlParameter("@ID", MySqlDbType.Int32)
        //    };
        //    parameters[0].Value = ID;

        //    int result = DbHelperMySQL.RunProcedure("p_objspecifictype_Exists", parameters, out rowsAffected);
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
        public bool Add(objSpecificType model)
        {
            string str = "insert into p_objspecifictype(TypeID,Name,Remake) value(@TypeID,@Name,@Remake);";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@TypeID", MySqlDbType.Int32,11),
                    new MySqlParameter("@Name", MySqlDbType.VarChar,255),
                    new MySqlParameter("@Remake", MySqlDbType.VarChar,255)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Remake;

            if (DBHelper.ExecuteSql(str, parameters) >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(objSpecificType model)
        {
            string str =
               "UPDATE p_objspecifictype SET TypeID=@TypeID,Name=@Name,Remake=@Remake where ID=@ID;";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@TypeID", MySqlDbType.Int32,11),
                    new MySqlParameter("@Name", MySqlDbType.VarChar,255),
                    new MySqlParameter("@Remake", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Remake;

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
            strSql.Append("delete from p_objspecifictype ");
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
        public objSpecificType GetModel(int ID)
        {
            string str = "SELECT * FROM p_objspecifictype WHERE ID=@ID; ";
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32)
            };
            parameters[0].Value = ID;

            objSpecificType model = new objSpecificType();
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
        public objSpecificType DataRowToModel(DataRow row)
        {
            objSpecificType model = new objSpecificType();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["TypeID"] != null && row["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
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
            strSql.Append("select ID,TypeID,Name,Remake ");
            strSql.Append(" FROM p_objspecifictype ");
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
            strSql.Append("select count(1) FROM p_objspecifictype ");
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
            strSql.Append(")AS Row, T.*  from p_objspecifictype T ");
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
			parameters[0].Value = "p_objspecifictype";
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
