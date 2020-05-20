using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using FeatureManagementTargetingSample.Blogs;

namespace FeatureManagementTargetingSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlogService _blogService;
        private readonly IFeatureManager _featureManager;

        public IndexModel(ILogger<IndexModel> logger, BlogService blogService, IFeatureManagerSnapshot featureManager)
        {
            _blogService = blogService;
            _featureManager = featureManager;
            _logger = logger;
        }

        public IEnumerable<Blog> RecentPosts { get; set; }

        public async Task OnGet()
        {
            if (await _featureManager.IsEnabledAsync(nameof(FeatureFlags.RecentPosts)))
            {
                RecentPosts = _blogService.GetRecentPosts();
            }
        }
    }
}
