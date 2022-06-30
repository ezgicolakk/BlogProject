using BlogProject.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.MODEL.Entities
{
    public class Category:CoreEntity
    {
        public Category()
        {
            Posts = new List<Post>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Navigation Properties

        //One category could have more than one post.
        public virtual List<Post> Posts { get; set; }
    }
}
