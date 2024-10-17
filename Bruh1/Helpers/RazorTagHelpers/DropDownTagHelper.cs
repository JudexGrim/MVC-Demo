using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement("Dropdown")]
    
    public class DropDownTagHelper : TagHelper
    {
        public string id { get; set; }
        public string Class { get; set; }
        public string DefaultClass { get; set; } = "btn-link dropdown-toggle";
        public IEnumerable<KeyValuePair<string, string>> Options { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            foreach (var option in Options)
            { 
                output.Content.AppendHtml($"\n<option value=\"{option.Value}\">{option.Key}</option>"); 
            }

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
                output.Attributes.SetAttribute("id", "edit-dropdown");
            }
        }
    }
}
