using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMyBlog
{
    /// <summary>
    /// p_objspecifictype:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class objSpecificType
    {
        public objSpecificType()
        { }
        #region Model
        private int _id;
        private int? _typeid;
        private string _name;
        private string _remake;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remake
        {
            set { _remake = value; }
            get { return _remake; }
        }
        #endregion Model

    }
}
