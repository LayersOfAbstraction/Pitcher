#pragma checksum "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9efdcee4bce7b038c8109b036b1c16629afe043"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__CookieConsentPartial), @"mvc.1.0.view", @"/Views/Shared/_CookieConsentPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_CookieConsentPartial.cshtml", typeof(AspNetCore.Views_Shared__CookieConsentPartial))]
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
#line 1 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\_ViewImports.cshtml"
using Pitcher;

#line default
#line hidden
#line 2 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\_ViewImports.cshtml"
using Pitcher.Models;

#line default
#line hidden
#line 1 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml"
using Microsoft.AspNetCore.Http.Features;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9efdcee4bce7b038c8109b036b1c16629afe043", @"/Views/Shared/_CookieConsentPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52358d1216eb874bde269b7aff6347ce38b7d0f9", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__CookieConsentPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Privacy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml"
  
    var consentFeature = Context.Features.Get<ITrackingConsentFeature>();
    var showBanner = !consentFeature?.CanTrack ?? false;
    var cookieString = consentFeature?.CreateConsentCookie();

#line default
#line hidden
            BeginContext(248, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 9 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml"
 if (showBanner)
{

#line default
#line hidden
            BeginContext(271, 7598, true);
            WriteLiteral(@"    <div id=""cookieConsent"" class=""alert alert-info alert-dismissible fade show"" role=""alert""><p>                     Jordan Nash built the Pitcher app as                     an Open Source app. This SERVICE is provided by                     Jordan Nash at no cost and is intended for                     use as is.                   </p> <p>                     This page is used to inform visitors regarding                     my policies with the collection, use, and                     disclosure of Personal Information if anyone decided to use                     my Service.                   </p> <p>                     If you choose to use my Service, then you agree                     to the collection and use of information in relation to this                     policy. The Personal Information that I collect is                     used for providing and improving the Service.                     I will not use or share your                     information with anyone except as described in this Priva");
            WriteLiteral(@"cy                     Policy.                   </p> <p>                     The terms used in this Privacy Policy have the same meanings                     as in our Terms and Conditions, which is accessible at                     Pitcher unless otherwise defined in this Privacy                     Policy.                   </p> <p><strong>Information Collection and Use</strong></p> <p>                     For a better experience, while using our Service,                     I may require you to provide us with certain                     personally identifiable information. The                     information that I request will be                     retained on your device and is not collected by me in any way.                   </p> <p>                     The app does use third party services that may collect                     information used to identify you.                   </p> <div><p>                       Link to privacy policy of third party service providers                       used by t");
            WriteLiteral(@"he app                     </p> <ul><li><a href=""https://www.google.com/policies/privacy/"" target=""_blank"">Google Play Services</a></li><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --><!-- --></ul></div> <p><strong>Log Data</strong></p> <p>                     I want to inform you that whenever                     you use my Service, in a case of an error in the                     app I collect data and information (through third                     party products) on your phone called Log Data. This Log Data                     may include information such as your device Internet                     Protocol (“IP”) address, device name, operating system                     version, the configuration of the app when utilizing                     my Service, the time and date of your use of the                     Service, and other statistics.                   </p> <p><strong>Cookies</strong></p> <p>                     Cookies are files with a small amount of");
            WriteLiteral(@" data that are                     commonly used as anonymous unique identifiers. These are                     sent to your browser from the websites that you visit and                     are stored on your device's internal memory.                   </p> <p>                     This Service does not use these “cookies” explicitly.                     However, the app may use third party code and libraries that                     use “cookies” to collect information and improve their                     services. You have the option to either accept or refuse                     these cookies and know when a cookie is being sent to your                     device. If you choose to refuse our cookies, you may not be                     able to use some portions of this Service.                   </p> <p><strong>Service Providers</strong></p> <p>                     I may employ third-party companies                     and individuals due to the following reasons:                   </p> <ul><li>To facilitat");
            WriteLiteral(@"e our Service;</li> <li>To provide the Service on our behalf;</li> <li>To perform Service-related services; or</li> <li>To assist us in analyzing how our Service is used.</li></ul> <p>                     I want to inform users of this                     Service that these third parties have access to your                     Personal Information. The reason is to perform the tasks                     assigned to them on our behalf. However, they are obligated                     not to disclose or use the information for any other                     purpose.                   </p> <p><strong>Security</strong></p> <p>                     I value your trust in providing us                     your Personal Information, thus we are striving to use                     commercially acceptable means of protecting it. But remember                     that no method of transmission over the internet, or method                     of electronic storage is 100% secure and reliable, and                     I cannot g");
            WriteLiteral(@"uarantee its absolute security.                   </p> <p><strong>Links to Other Sites</strong></p> <p>                     This Service may contain links to other sites. If you click                     on a third-party link, you will be directed to that site.                     Note that these external sites are not operated by                     me. Therefore, I strongly advise you to                     review the Privacy Policy of these websites.                     I have no control over and assume no                     responsibility for the content, privacy policies, or                     practices of any third-party sites or services.                   </p> <p><strong>Children’s Privacy</strong></p> <p>                     These Services do not address anyone under the age of 13.                     I do not knowingly collect personally                     identifiable information from children under 13. In the case                     I discover that a child under 13 has provided                ");
            WriteLiteral(@"     me with personal information,                     I immediately delete this from our servers. If you                     are a parent or guardian and you are aware that your child                     has provided us with personal information, please contact                     me so that I will be able to do                     necessary actions.                   </p> <p><strong>Changes to This Privacy Policy</strong></p> <p>                     I may update our Privacy Policy from                     time to time. Thus, you are advised to review this page                     periodically for any changes. I will                     notify you of any changes by posting the new Privacy Policy                     on this page. These changes are effective immediately after                     they are posted on this page.                   </p> <p><strong>Contact Us</strong></p> <p>                     If you have any questions or suggestions about                     my Privacy Policy, do not hesitate to c");
            WriteLiteral(@"ontact                     me at jnash51@live.com.                   </p> <p>                     This privacy policy page was created at                     <a href=""https://privacypolicytemplate.net"" target=""_blank"">privacypolicytemplate.net</a>                     and modified/generated by                     <a href=""https://app-privacy-policy-generator.firebaseapp.com/"" target=""_blank"">App Privacy Policy Generator</a></p>");
            EndContext();
            BeginContext(7869, 72, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9efdcee4bce7b038c8109b036b1c16629afe04313084", async() => {
                BeginContext(7927, 10, true);
                WriteLiteral("Learn More");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7941, 121, true);
            WriteLiteral(".\r\n        <button type=\"button\" class=\"accept-policy close\" data-dismiss=\"alert\" aria-label=\"Close\" data-cookie-string=\"");
            EndContext();
            BeginContext(8063, 12, false);
#line 12 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml"
                                                                                                                 Write(cookieString);

#line default
#line hidden
            EndContext();
            BeginContext(8075, 403, true);
            WriteLiteral(@""">
            <span aria-hidden=""true"">Accept</span>
        </button>
    </div>
    <script>
        (function () {
            var button = document.querySelector(""#cookieConsent button[data-cookie-string]"");
            button.addEventListener(""click"", function (event) {
                document.cookie = button.dataset.cookieString;
            }, false);
        })();
    </script>
");
            EndContext();
#line 24 "E:\Work\Programming Experiments\ASP.NET CORE\Pitcher\application\ContosoUniversity\Views\Shared\_CookieConsentPartial.cshtml"
}

#line default
#line hidden
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