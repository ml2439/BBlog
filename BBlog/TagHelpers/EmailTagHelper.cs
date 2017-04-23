using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace BBlog.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "gmail.com";

        // Authoring tag helpers
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
            var content = await output.GetChildContentAsync();
            var address = content.GetContent() + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}