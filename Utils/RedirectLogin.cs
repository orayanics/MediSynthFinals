using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediSynthFinals.Utils
{
    public class RedirectLogin : RedirectToPageResult
    {
        public RedirectLogin(string pageName) : base(pageName)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            // Customize the redirect logic based on the user's role
            // You can access the user's role from HttpContext.User.Identity

            // Example: Redirect to a custom page for "admin" role
            if (context.HttpContext.User.IsInRole("ADMIN"))
            {
                // Redirect to the custom admin page
                PageName = "/User/Login";
            }

            return base.ExecuteResultAsync(context);
        }
    }
}
