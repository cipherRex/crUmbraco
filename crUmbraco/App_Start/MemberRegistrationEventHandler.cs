using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace crUmbraco.EventHandlers
{
    public class MemberRegistrationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            MemberService.Created += MemberService_Created;
        }

        private void MemberService_Created(IMemberService sender, NewEventArgs<IMember> e)
        {
            // Always add user to "Main Client" group
            sender.AssignRole(e.Entity.Username, "Main Client");
            sender.Save(e.Entity);
        }
    }
}