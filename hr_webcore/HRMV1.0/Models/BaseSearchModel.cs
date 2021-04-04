using System;
using System.Text;

namespace HRM.Models
{
    public class BaseListModel
    {        
        public BaseListModel()
        {
            IsNotPaging = false;
        }

        public bool IsNotPaging { get; set; }
        public int PageIndex {
            get
            {
                var p = System.Web.HttpContext.Current.Request["pageIndex"];
                return p != null ? int.Parse(p) : 1;
            }
        }
        public int PageSize
        {
            get
            {
                if (IsNotPaging) return 99999999;
                var p = System.Web.HttpContext.Current.Request["pagesize"];
                return p != null ? int.Parse(p) : 50;
            }
            set { }
        }
        public int TotalItem { get; set; }       

        public string GetHtmlPaging
        {
            get
            {
                var str = new StringBuilder();

                str.Append("<div class=\"text-align-right\">");
                str.Append("<ul class=\"pagination\">");
                
                var strUrl = System.Web.HttpContext.Current.Request.RawUrl;
                if (System.Web.HttpContext.Current.Request.QueryString.Count == 0)
                {
                    strUrl += "?pageIndex={0}";
                }
                else
                {
                    if (strUrl.IndexOf(string.Format("&pageIndex={0}", PageIndex), System.StringComparison.Ordinal) > 0)
                    {
                        strUrl = strUrl.Replace(string.Format("&pageIndex={0}", PageIndex), "&pageIndex={0}");
                    }
                    else if (strUrl.IndexOf(string.Format("?pageIndex={0}", PageIndex), System.StringComparison.Ordinal) >0)
                    {
                        strUrl = strUrl.Replace(string.Format("?pageIndex={0}", PageIndex), "?pageIndex={0}");
                    }
                    else if(System.Web.HttpContext.Current.Request["pageIndex"]==null)
                    {
                        strUrl += "&pageIndex={0}";
                    }
                }

                if (TotalItem <=PageSize)
                {
                    return string.Empty;
                }

                if (PageSize == 0 || TotalItem == 0)
                {
                    return string.Empty;
                }
                var totalPage = TotalItem / PageSize;
                var dongdu = TotalItem % PageSize;
                if (dongdu != 0)
                {
                    totalPage = totalPage + 1;
                }

                if (totalPage == 1)
                {
                    return string.Empty;
                }   

                if (PageIndex != 1)
                {
                    str.AppendFormat("<li><a href='{0}' onclick=\"_global.unBlockUI('#content')\"><i class=\"fa fa-angle-left\"></i></a></li>", string.Format(strUrl, PageIndex - 1));
                }

                for (int i = 1; i <= totalPage; i++)
                {
                    if (i == PageIndex)
                    {
                        str.AppendFormat("<li class='active'><a href='javascript:void(0)' onclick=\"_global.unBlockUI('#content')\">{0}</a></li>", i);
                    }
                    else
                    {
                        if (i >= PageIndex - 5 && i <= PageIndex + 5)
                        {
                            str.AppendFormat("<li><a href='{0}' onclick=\"_global.unBlockUI('#content')\">{1}</a></li>", string.Format(strUrl, i), i);
                        }
                    }
                }

                if (PageIndex != totalPage)
                {
                    str.AppendFormat("<li><a href='{0}' onclick=\"_global.unBlockUI('#content')\"><i class=\"fa fa-angle-right\"></i></a></li>", string.Format(strUrl, PageIndex + 1));
                }

                str.Append("</div>");
                str.Append("</ul>");
                return str.ToString();
            }
        }
    }
}
