using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public partial class Games
    {
        public int Id { get; set; }
        public string NameGame { get; set; }
        public string DescriptionGame { get; set; }
        public string ImageGame { get; set; }
        public int LimitUserGames { get; set; }
    }
}
