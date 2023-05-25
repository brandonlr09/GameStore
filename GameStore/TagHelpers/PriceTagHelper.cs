using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GameStore.TagHelpers
{
    [HtmlTargetElement("currency")]
    public class PriceTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "currency";

            var productInfo = output.GetChildContentAsync().Result;
            var productPrice = productInfo.GetContent();

            var renderedPrice = decimal.Parse(productPrice).ToString("C"); //Currency formatting
            var region = " CAD";

            output.Content.SetContent(renderedPrice + region);
        }
    }
}
