using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public class PortfolioItem
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public int Stars { get; set; }
        public DateTimeOffset? LastCommit { get; set; }
        public string Url { get; set; }
    }
}
