﻿namespace Hedaya.Domain.Entities
{
    public class Reply 
    {
        public Reply()
        {
             
        }

        public int Id { get; set; }
        public required string Text { get; set; }
        public string ImagePath { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
