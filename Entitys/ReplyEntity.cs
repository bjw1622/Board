﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class ReplyEntity
    {
        public int BoardNum { get; set; }
        public int ReplyID { get; set; }
        public string ReplyContent { get; set; }
    }
}