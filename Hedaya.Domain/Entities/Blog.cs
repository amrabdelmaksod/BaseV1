﻿namespace Hedaya.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
