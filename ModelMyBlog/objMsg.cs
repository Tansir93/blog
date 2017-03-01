using System;
namespace ModelMyBlog
{
    /// <summary>
    /// p_objmsg:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class objMsg
    {
        public objMsg()
        { }
        #region Model
        private int _id;
        private int _specifictypeid;
        private string _title;
        private string _msg;
        private DateTime? _creatdatetime;
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
        public int SpecificTypeID
        {
            set { _specifictypeid = value; }
            get { return _specifictypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatDateTime
        {
            set { _creatdatetime = value; }
            get { return _creatdatetime; }
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

