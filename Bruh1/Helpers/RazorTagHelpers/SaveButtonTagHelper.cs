﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement ("SaveButton")]
    [HtmlTargetElement("SubmitButton")]
    public class SaveButtonTagHelper : TagHelper
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public string? Class { get; set; }
        public string DefaultClass { get; set; } = "btn-submit";

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

            if (type != null)
            {
                output.Attributes.SetAttribute("type", type);
            }
            else
            {
                output.Attributes.SetAttribute("type", "button");
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
                output.Attributes.SetAttribute("id", "save-btn");
            }
        }
    }
}
