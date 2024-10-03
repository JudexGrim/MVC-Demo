using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Web;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement("EditButton")]
    public class EditButtonTagHelper : TagHelper
    {
        public string id { get; set; }
        public string Class { get; set; }
        public string DefaultClass { get; set; } = "btn-info rounded-1 px-3 py-1 text-decoration-none border-0";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Content.SetContent("Edit");
            output.Attributes.SetAttribute("type", "button");

            if (Class != null)
            {
                output.Attributes.SetAttribute("class", Class + " " + DefaultClass);
            }
            else
            {
                output.Attributes.SetAttribute("class", DefaultClass);
            }

            if (id != null)
            {
                output.Attributes.SetAttribute("id", id);
            }
            else
            {
                output.Attributes.SetAttribute("id", "edit-btn");
            }
        }
    }
}