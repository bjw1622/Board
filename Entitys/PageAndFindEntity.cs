using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class PageAndFindEntity
    {
        public string Variable { get; set; }
        public string Input { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}