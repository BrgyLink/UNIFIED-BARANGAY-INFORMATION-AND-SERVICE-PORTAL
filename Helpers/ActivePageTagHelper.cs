using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace BrgyLink.Helpers
{
    public class ActivePageTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActivePageTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("asp-page")]
        public string Page { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentPage = _httpContextAccessor.HttpContext.Request.Path;

            if (currentPage.Value.Contains(Page))
            {
                var existingClasses = output.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
                output.Attributes.SetAttribute("class", $"{existingClasses} active");
            }
        }
    }
}
