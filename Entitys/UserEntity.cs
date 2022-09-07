using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Board.Entitys
{
    public class UserEntity
    {
        [Required]
        public int No { get; set; }
        [Required]
        public string Email { get; set; }
        public string Pw { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Birth { get; set; }
        public string CreateDate { get; set; }
    }
}