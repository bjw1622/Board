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
        public string Email { get; set; }
        [Required]
        public string Pw { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Birth { get; set; }
    }
}