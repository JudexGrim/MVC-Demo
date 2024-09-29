using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web;
using CoreLib;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Helpers
{
    public class CustomHtmlHelpers : Disposer
    {
        public static HtmlString EditButton(string id = null, string onclick = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("Button");
            tagBuilder.InnerHtml.SetContent("Edit");
            tagBuilder.Attributes.Add("type", "button");

            tagBuilder.Attributes.Add("class", "btn-info rounded-1 px-3 py-1 text-decoration-none border-0");
            tagBuilder.Attributes.Add("style","");

            if(id != null)

            {
                tagBuilder.Attributes.Add("id", id); 
            }
            if(onclick != null)

            {
                tagBuilder.Attributes.Add("onclick",onclick);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString CreateButton(string id = null, string onclick = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("Button");
            tagBuilder.InnerHtml.SetContent("Create");
            tagBuilder.Attributes.Add("type", "button");

            tagBuilder.Attributes.Add("class", "btn-primary rounded-1 px-3 py-1 text-decoration-none border-0");
            tagBuilder.Attributes.Add("style", "");

            if (id != null)

            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (onclick != null)

            {
                tagBuilder.Attributes.Add("onclick", onclick);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString DeleteButton(string id = null, string onclick = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("Button");
            tagBuilder.InnerHtml.SetContent("Delete");
            tagBuilder.Attributes.Add("type", "button");

            tagBuilder.Attributes.Add("class", "btn-danger rounded-1 px-3 py-1 text-decoration-none border-0");
            tagBuilder.Attributes.Add("style", "");

            if (id != null)

            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (onclick != null)

            {
                tagBuilder.Attributes.Add("onclick", onclick);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString SaveButton(string id = null, string onclick = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("Button");
            tagBuilder.InnerHtml.SetContent("Save");
            tagBuilder.Attributes.Add("type", "submit");

            tagBuilder.Attributes.Add("class", "btn-success rounded-1 px-3 py-1 text-decoration-none border-0");
            tagBuilder.Attributes.Add("style", "");

            if (id != null)

            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (onclick != null)

            {
                tagBuilder.Attributes.Add("onclick", onclick);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString CancelButton(string id = null, string onclick = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("Button");
            tagBuilder.InnerHtml.SetContent("Cancel");
            tagBuilder.Attributes.Add("type", "reset");

            tagBuilder.Attributes.Add("class", "btn-warning rounded-1 px-3 py-1 text-decoration-none border-0");
            tagBuilder.Attributes.Add("style", "");

            if (id != null)

            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (onclick != null)

            {
                tagBuilder.Attributes.Add("onclick", onclick);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString InputText(bool required, string id = null, string name = null, string value = null, string keyDownEvent = null, IEnumerable<KeyValuePair<string,string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("input");

            tagBuilder.Attributes.Add("type","text");
            tagBuilder.Attributes.Add("class", "");
            tagBuilder.Attributes.Add("style", "");

            if (required)
            {
                tagBuilder.Attributes.Add("required", "required");
            }

            if (id != null)
            {
                tagBuilder.Attributes.Add("id", id);
            }
            
            if(name != null)
            {
                tagBuilder.Attributes.Add("name", name);
            }
            
            if(value != null)
            {
                tagBuilder.Attributes.Add("value", value);
            }
            
            if(keyDownEvent != null)
            {
                tagBuilder.Attributes.Add("onkeydown", keyDownEvent);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString InputNumber(bool required, string id = null, string name = null, string value = null, string keyDownEvent = null, string  decimalSteps = null, string minVal = null, string maxVal = null, IEnumerable<KeyValuePair<string, string>> dataSelectors = null)
        {
            TagBuilder tagBuilder = new TagBuilder("input");

            tagBuilder.Attributes.Add("type", "number");
            tagBuilder.Attributes.Add("class", "");
            tagBuilder.Attributes.Add("style", "");

            if (required)
            {
                tagBuilder.Attributes.Add("required", "required");
            }

            if (id != null)
            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (name != null)
            {
                tagBuilder.Attributes.Add("name", name);
            }

            if (value != null)
            {
                tagBuilder.Attributes.Add("value", value);
            }

            if (keyDownEvent != null)
            {
                tagBuilder.Attributes.Add("onkeydown", keyDownEvent);
            }

            if (decimalSteps != null)
            {
                tagBuilder.Attributes.Add("step", decimalSteps); 
            }

            if (minVal != null)
            {
                tagBuilder.Attributes.Add("min", minVal);
            }

            if (maxVal != null)
            {
                tagBuilder.Attributes.Add("max", maxVal);
            }

            if (dataSelectors != null)
            {
                foreach (var selector in dataSelectors)
                {
                    tagBuilder.Attributes.Add(selector.Key, selector.Value);
                }
            }
            return new HtmlString(tagBuilder.ToString());
        }
    }
}
