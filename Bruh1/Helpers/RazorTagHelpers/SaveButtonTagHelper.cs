using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement ("SaveButton")]
    [HtmlTargetElement("SubmitButton")]
    public class SaveButtonTagHelper : TagHelper
    {
        public string id { get; set; }
        public string Class { get; set; }
        public string DefaultClass { get; set; } = "btn-success rounded-1 px-3 py-1 text-decoration-none border-0";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            
            var content = output.GetChildContentAsync().Result.GetContent();
            if (string.IsNullOrWhiteSpace(content))
            {
                output.Content.SetContent("Save");
            }
            else 
            { 
                output.Content.SetContent(content);
            }


            output.Attributes.SetAttribute("type", "submit");


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
                output.Attributes.SetAttribute("id", "save-btn");
            }
        }
    }
}
