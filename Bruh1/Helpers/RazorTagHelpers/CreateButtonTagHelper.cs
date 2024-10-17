using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement("CreateButton")]
    public class CreateButtonTagHelper : TagHelper
    {
        public string id { get; set; }

        public string Class { get; set; }
        public string DefaultClass { get; set; } = "btn-create";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Content.SetContent("Create");

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
                output.Attributes.SetAttribute("id", "create-btn");
            }
        }
    }
}
