using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Board.Entitys
{
    public class BoardEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BoardNum { get; set; }
        public string Title { get; set; }
        public string MainContent { get; set; }
        public string Name { get; set; }
        public int ReplyCount { get; set; }
        public int RecommandCount { get; set; }
    }
}