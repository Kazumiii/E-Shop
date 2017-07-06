using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.HtmlHelpers
{
//Here I generate HTML for a set of pagelinks using information provided in PaginInfo objec
    public  static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,PagingInfo pagingInfo,Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CuretnPage)
                    tag.AddCssClass("Selected");
                result.Append(tag.ToString());

            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}