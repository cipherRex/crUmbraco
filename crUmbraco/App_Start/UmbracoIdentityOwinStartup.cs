using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Configuration;
using System.Linq;
using UmbracoIdentity; 
using crUmbraco;
using crUmbraco.Models.UmbracoIdentity;
//using Microsoft.Owin.Security.Interop;
//using Microsoft.AspNetCore.DataProtection;

[assembly: OwinStartup("UmbracoIdentityStartup", typeof(UmbracoIdentityOwinStartup))]
namespace crUmbraco
{

    /// <summary>
    /// OWIN Startup class for UmbracoIdentity 
    /// </summary>
    public class UmbracoIdentityOwinStartup : UmbracoIdentityOwinStartupBase
    {
        protected override void ConfigureUmbracoUserManager(IAppBuilder app)
        {
            base.ConfigureUmbracoUserManager(app);

            //Single method to configure the Identity user manager for use with Umbraco
            app.ConfigureUserManagerForUmbracoMembers<UmbracoApplicationMember>();

            //Single method to configure the Identity user manager for use with Umbraco
            app.ConfigureRoleManagerForUmbracoMembers<UmbracoApplicationRole>();
        }

        protected override void ConfigureUmbracoAuthentication(IAppBuilder app)
        {
            base.ConfigureUmbracoAuthentication(app);

            // Enable the application to use a cookie to store information for the 
            // signed in user and to use a cookie to temporarily store information 
            // about a user logging in with a third party login provider 
            // Configure the sign in cookie
            
            var cookieOptions = CreateFrontEndCookieAuthenticationOptions();
            //cookieOptions.CookieName = ".AspNet.SharedCookie";

            // You can change the cookie options here. The cookie options will be automatically set
            // based on what is configured in the security section of umbracoSettings.config and the web.config.
            // For example:
             cookieOptions.CookieName = "cipherRex";
            //cookieOptions.AuthenticationType = "Identity.Application";
            // cookieOptions.ExpireTimeSpan = TimeSpan.FromDays(20);


            cookieOptions.Provider = new CookieAuthenticationProvider
            {
                // Enables the application to validate the security stamp when the user 
                // logs in. This is a security feature which is used when you 
                // change a password or add an external login to your account.  
                OnValidateIdentity = SecurityStampValidator
                        .OnValidateIdentity<UmbracoMembersUserManager<UmbracoApplicationMember>, UmbracoApplicationMember, int>(
                            TimeSpan.FromMinutes(30),
                            (manager, user) => user.GenerateUserIdentityAsync(manager),
                            identity => identity.GetUserId<int>()),
            };

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Identity.Application",
            //    CookieName = ".AspNet.SharedCookie",
            //    LoginPath = new PathString("/Account/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        OnValidateIdentity =
            //            SecurityStampValidator
            //                .OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //                    validateInterval: TimeSpan.FromMinutes(30),
            //                    regenerateIdentity: (manager, user) =>
            //                        user.GenerateUserIdentityAsync(manager))
            //    },
            //    TicketDataFormat = new AspNetTicketDataFormat(
            //        new DataProtectorShim(
            //            DataProtectionProvider.Create("{PATH TO COMMON KEY RING FOLDER}",
            //                (builder) => { builder.SetApplicationName("SharedCookieApp"); })
            //            .CreateProtector(
            //                "Microsoft.AspNetCore.Authentication.Cookies." +
            //                    "CookieAuthenticationMiddleware",
            //                "Identity.Application",
            //                "v2"))),
            //    CookieManager = new ChunkingCookieManager()
            //});

            app.UseCookieAuthentication(cookieOptions, PipelineStage.Authenticate);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //app.UseMicrosoftAccountAuthentication(
            //  clientId: "",
            //  clientSecret: "");

            //app.UseTwitterAuthentication(
            //  consumerKey: "",
            //  consumerSecret: "");

            //app.UseFacebookAuthentication(
            //  appId: "",
            //  appSecret: "");

            app.UseGoogleAuthentication(
              clientId: "57285695237-h8t84k0ctntkjadoge96hv198giie10s.apps.googleusercontent.com",
              clientSecret: "o2MlfkjFu7chpK3Bty9gkU0j");


        }
    }
}

