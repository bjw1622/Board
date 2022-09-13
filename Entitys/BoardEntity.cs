using System;

namespace Board.Entitys
{
    public class BoardEntity
    {
        public int No { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int User_No { get; set; }
    }
}