using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {

          
            List<objMsg> listMsgs = bll.GetModelList(" 1=1 order by CreatDateTime LIMIT 10 ");
            List<objSpecificType> listSpecificType = bllSpecificType.GetModelList(" TypeID=1 ");

          //  string str= 
            //objMsg obj = new objMsg();

            //obj.CreatDateTime = DateTime.Now;
            
            //obj.Msg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));
            //obj.SpecificTypeID = 1;
            //obj.Title = "数据结构与算法分析数据结构与算法分析数据结构与算法分析数据结构与算法分析";
            //bll.Add(obj);


            List<objMsg> list = listMsgs;

            List<objSpecificType> ListBook = listSpecificType;

            ViewData["NewMsg"] = list;
            ViewData["book"] = ListBook;

            return View();
        }


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
            ViewData["NewMsg"] = listMsgs;
            ViewData["book"] = ListBook;
            return View();
        }

        public ActionResult Content(string ID)
        {
            List<objSpecificType> ListBook = bllSpecificType.GetModelList(" TypeID=1 ");
            ViewData["book"] = ListBook;
            return View();
        }

    }
}