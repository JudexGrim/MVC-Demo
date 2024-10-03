﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Helpers.RazorTagHelpers
{
    [HtmlTargetElement ("SaveButton")]
    public class SaveButtonTagHelper : TagHelper
    {
        public string id { get; set; }
        public string Class { get; set; }
        public string DefaultClass { get; set; } = "btn-success rounded-1 px-3 py-1 text-decoration-none border-0";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Content.SetContent("Save");

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
