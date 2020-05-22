using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagementTargetingSample
{
    public class TargetingContextAccesor : ITargetingContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TargetingContextAccesor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var result = new TargetingContext();
            result.UserId = httpContext.User.Identity.Name;

            if (httpContext.User.HasClaim("Insider", "true"))
            {
                result.Groups = new string[] { "Insiders" };
            }
            return new ValueTask<TargetingContext>(result);
        }

    }
}
