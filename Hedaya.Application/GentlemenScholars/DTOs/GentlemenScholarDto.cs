﻿namespace Hedaya.Application.GentlemenScholars.DTOs
{
    public class GentlemenScholarDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
        public string? ImageUrl { get; set; }
    }
}
