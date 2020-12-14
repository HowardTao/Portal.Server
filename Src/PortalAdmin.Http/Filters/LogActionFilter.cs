using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PortalAdmin.HttpApi.Filters
{
    public class LogActionFilter:IAsyncActionFilter
    {
        private readonly 
        
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}