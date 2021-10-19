using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Jobit.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jobit.Web.Infrastructure
{
    [HtmlTargetElement("label", Attributes = "gender-description")]
    public class DescriptionsTagHelper : TagHelper
    {
        [HtmlAttributeName("gender-description")]
        public string gender { get; set; }

        public override  async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            object objTestEnum;
            objTestEnum = Enum.Parse(Genders.male.GetType(), gender);
            output.Content.SetContent(((Genders)objTestEnum).ToDescription());
        }

    }
}
