#pragma checksum "C:\Users\Asifs\Downloads\Compressed\RugerRumble\RugerRumble\RugerRumble\Views\Account\EmailVerification.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4b4090ee5f8cbc4221ee603b7765af8ff6787267"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_EmailVerification), @"mvc.1.0.view", @"/Views/Account/EmailVerification.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Asifs\Downloads\Compressed\RugerRumble\RugerRumble\RugerRumble\Views\_ViewImports.cshtml"
using RugerRumble;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Asifs\Downloads\Compressed\RugerRumble\RugerRumble\RugerRumble\Views\_ViewImports.cshtml"
using RugerRumble.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b4090ee5f8cbc4221ee603b7765af8ff6787267", @"/Views/Account/EmailVerification.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ce2cfd35167a516f15d11745749892ce08cfce51", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_EmailVerification : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Asifs\Downloads\Compressed\RugerRumble\RugerRumble\RugerRumble\Views\Account\EmailVerification.cshtml"
  
    ViewData["Title"] = "EmailVerification";
    Layout = "~/Views/Shared/_LayoutCustom.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main class=""main-content"" style=""background-color: #2b2b2b;box-shadow:15px;"">
    <div class=""container"">
        <div class=""page"">
            <section>
                <div style=""border-radius:20px"">
                    <div>
                        <div class=""row"" style=""padding-bottom:10px;"">
                            <div class=""col-md-6"" style=""padding:30px"">
                                <h2 style=""color:red"">Email verification required.</h2>
                                <br />
                                <p style=""color:lawngreen"">We have sent a mail with activation link. Please check your email and click the link to activate your account.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</main>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
