using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class BoardFileEntity
    {
        public int BoardNum { get; set; }
        public string Title { get; set; }
        public string MainContent { get; set; }
        public string Name { get; set; }
        public int ReplyCount { get; set; }
        public int RecommandCount { get; set; }
        public int FileNum { get; set; }
        public string FileName { get; set; }
        public string FileSaveName { get; set; }
    }
}