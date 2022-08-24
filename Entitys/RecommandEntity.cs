using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class RecommandEntity
    {
        public int BoardNum { get; set; }
        public string Email { get; set; }
        public int Recommand { get; set; }
        public int RecommandCount { get; set; }
    }
}