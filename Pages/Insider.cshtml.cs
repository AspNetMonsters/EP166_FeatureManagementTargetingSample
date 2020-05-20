using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeatureManagementTargetingSample.Pages
{
    [Authorize]
    public class InsiderModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public InsiderModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool IsInsider { get; set; }

        public async Task OnGetAsync()
        {
            IsInsider = User.HasClaim("Insider", "true");
        }

        public async Task<ActionResult> OnPostJoinAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.AddClaimAsync(user, new Claim("Insider", "true"));
            await _signInManager.RefreshSignInAsync(user);

            

            return RedirectToPage("./Insider");
        }
    }
}