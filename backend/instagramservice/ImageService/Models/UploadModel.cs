using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.picsfeed.ImageService.Models
{
    public class UploadModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SavePath { get; set; }

        public virtual IList<string> Tags { get {
            List<string> tags = new List<string>();
            if (!string.IsNullOrEmpty(Description)) {
                var regex = new Regex(@"#\w+");
                var tg = regex.Matches(Description);
                if (tg != null && tg.Any())
                {
                    tags.AddRange(tg.Select(x => x.Value));
                }
            }
            return tags;
        } }
    }
}
