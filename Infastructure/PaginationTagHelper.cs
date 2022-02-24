using BookstoreProj.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProj.Infastructure
{
    //HTML Target element is going to be a DIV, and so we are building an action called page-blah
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //dynamically create the page links for us
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        //different than ViewContext
        //the page-blah from the index page is being passed into this variable 
        public PageInfo PageBlah {get; set;}
        public string PageAction { get; set; }

        //a tool that will help us build these tags dynamically 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i < (PageBlah.TotalPages); i++)
            {
                TagBuilder tb = new TagBuilder("a");

                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            output.Content.AppendHtml(final.InnerHtml);
        }
    }
}
