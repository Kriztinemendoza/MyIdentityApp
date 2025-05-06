using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentityApp.Models
{
    public class Scripture
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Text { get; set; }
        public string IdentityTheme { get; set; }
        public string Affirmation { get; set; }
        public bool IsFavorite { get; set; }

        public string Reflection { get; set; }
    }

    public class IdentityTheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
