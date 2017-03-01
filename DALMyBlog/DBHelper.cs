using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DALMyBlog
{
  public  class DBHelper
  {
      public static string connectionString = "Database = mybolg; Data Source = localhost; User Id =root; Password=123456;pooling=false;CharSet=utf8;port=3306";
        //"Host=localhost;" +
        //                                        "DataBase=mybolg;" +
        //                                        "Protocol=TCP;" +
        //                                        "Port=3306;" +
        //                                        "Direct=true;" +
        //                                        "Compress=false;" +
        //                                        "Pooling=true;" +
        //                                        "Min Pool Size=0;" +
        //                                        "Max Pool Size=100;" +
        //                                        "Connection Lifetime=0;" +
        //                                        "User id=root;" +
        //                                        "Password=123456;";
        //User Id=root;Host=localhost;Character Set=compatibility
        //   //"server=localhost;User Id=root;password=123456;Database=myblog";
        //  ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ExecuteSql(string str, MySqlParameter[] parameters=null)
      {
          using (MySqlConnection conn = new MySqlConnection(connectionString))
          {
              using (MySqlCommand cmd=new MySqlCommand(str,conn))
              {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                 return cmd.ExecuteNonQuery();
              }
          }
      }
        /// <summary>
        /// 返回第一个行数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
      public static object GetSingle(string str, MySqlParameter[] parameters = null)
      {
          using (MySqlConnection conn=new MySqlConnection(connectionString))
          {
              using (MySqlCommand cmd=new MySqlCommand(str,conn))
              {
                    cmd.Parameters.AddRange(parameters);
                  conn.Open();
                 return cmd.ExecuteScalar();
              }
          }
      }
        /// <summary>
        /// 返回整个查询数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
      public static DataSet Query(string str, MySqlParameter[] parameters = null)
      {
          using (MySqlConnection conn=new MySqlConnection(connectionString))
          {
            DataSet ds=new DataSet();
                conn.Open();
              using (MySqlCommand cmd = new MySqlCommand(str, conn))
              {
                  if (parameters!=null)
                  {
                        cmd.Parameters.AddRange(parameters);
                    }
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds, "ds");
                }
                   
              return ds;
          }
      }


    }
}
