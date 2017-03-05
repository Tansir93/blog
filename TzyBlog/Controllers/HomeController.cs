using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BLLMyBlog;
using ModelMyBlog;

namespace TzyBlog.Controllers
{
    public class HomeController : Controller
    {
        private BLLobjMsg bll = BLLobjMsg.Init();
        private BLLSpecificType bllSpecificType = BLLSpecificType.Init();
        // GET: Home
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

          
            List<objMsg> listMsgs = bll.GetModelList(" 1=1 order by CreatDateTime LIMIT 10 ");
            List<objSpecificType> listSpecificType = bllSpecificType.GetModelList(" TypeID=1 ");

            List<objMsg> list = listMsgs;

            List<objSpecificType> ListBook = listSpecificType;

            ViewData["NewMsg"] = objMsgBase64ToString(list);
            ViewData["book"] = ListBook;

            return View();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult List(string ID)
        {
            if (ID==null)
            {
                ViewData["Title1"] = "非法登陆";
                ViewData["NewMsg"] = new List<objMsg>();
                ViewData["book"] = new List<objSpecificType>();
                return View();
            }
            List<objSpecificType> ListBook = bllSpecificType.GetModelList(" TypeID=1 ");

            objSpecificType obj = bllSpecificType.GetModel(int.Parse(ID));

            List<objMsg> listMsgs = bll.GetModelList(" 1=1 and SpecificTypeID="+ID+"");
            ViewData["Title1"] = obj.Name;
            ViewData["NewMsg"] = objMsgBase64ToString(listMsgs);
            ViewData["book"] = ListBook;
            return View();
        }

        /// <summary>
        /// 内容页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Content(string ID)
        {
            List<objSpecificType> ListBook = bllSpecificType.GetModelList(" TypeID=1 ");
            ViewData["book"] = ListBook;

         objMsg msg=bll.GetModel(int.Parse(ID));
            if (msg!=null)
            {
                msg.Title = FromUnicodeString(msg.Title);
                msg.Msg = FromUnicodeString(msg.Msg);
            }
            ViewData["msg"] = msg;
            return View();
        }


        public ActionResult AddContent()
        {
            List<objSpecificType> ListBook = bllSpecificType.GetModelList(" TypeID=1 ");
            ViewData["book"] = ListBook;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult SaveContent(string username, string password, string tbTilie, string tbMsg,string tbType)
        {
            if (username=="tanzhenyu"&&password=="liuyang")
            {
                objMsg obj = new objMsg();

                obj.CreatDateTime = DateTime.Now;

                obj.Msg = ToUnicodeString(tbMsg);//Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tbMsg));
                obj.SpecificTypeID = int.Parse(tbType);
                obj.Title = ToUnicodeString(tbTilie);//Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tbTilie)); ;
                bll.Add(obj);
            }
            List<objMsg> listMsgs = bll.GetModelList(" 1=1 order by CreatDateTime LIMIT 10 ");
            List<objSpecificType> listSpecificType = bllSpecificType.GetModelList(" TypeID=1 ");

            List<objMsg> list = listMsgs;

            List<objSpecificType> ListBook = listSpecificType;

            ViewData["NewMsg"] = objMsgBase64ToString(list);
            ViewData["book"] = ListBook;
            return View("Index");
        }

        public List<objMsg> objMsgBase64ToString(List<objMsg> listMsg)
        {
            foreach (objMsg objMsg in listMsg)
            {
                objMsg.Title = FromUnicodeString(objMsg.Title);
                objMsg.Msg = FromUnicodeString(objMsg.Msg);
            }
            return listMsg;
        }

        public string Base64ToString(string str)
        {
            byte[] c = Convert.FromBase64String(str);
            return System.Text.Encoding.Default.GetString(c);
        }


        #region /////
        public static string ToUnicodeString(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }

        public static string FromUnicodeString(string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }
#endregion

    }
}