using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagementTargetingSample.Blogs
{
    public class BlogService
    {
        public IEnumerable<Blog> GetRecentPosts()
        {
            return A.ListOf<Blog>(20);
        }
    }
}
