using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class UserEntity
    {
        public int No { get; set; }
        public string Email { get; set; }
        public string Pw { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
        public string CreateDate { get; set; }
    }
}