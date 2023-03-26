﻿using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class FilterCoursesQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
        public List<int> CategoryIds { get; set; }
        public bool SortByDurationAscending { get; set; }
        public string? searchKeyword { get; set; }

        public class FilterCoursesQueryHandler : IRequestHandler<FilterCoursesQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public FilterCoursesQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<object> Handle(FilterCoursesQuery request, CancellationToken cancellationToken)
            {
                // Filter by category IDs if provided
                IQueryable<Course> query = _context.Courses;
                if (request.CategoryIds != null && request.CategoryIds.Count > 0)
                {
                    query = query.Where(x => request.CategoryIds.Contains(x.SubCategoryId));
                }

         
                // Apply search filter
                if (!string.IsNullOrEmpty(request.searchKeyword))
                {
                    query = query.Where(x => x.TitleEn.Contains(request.searchKeyword) || x.TitleAr.Contains(request.searchKeyword) || x.Description.Contains(request.searchKeyword));
                }

                // Sort by duration
                if (request.SortByDurationAscending)
                {
                    query = query.OrderBy(x => x.Duration);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Duration);
                }

                // Get total count for pagination
                int totalCount = await query.CountAsync(cancellationToken);

                // Get paginated results
                int skip = (request.PageNumber - 1) * 10;
                var courses = await query.Skip(skip)
                    .Take(10)
                    .Select(c=> new CourseDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToString("d"),
                        Duration = c.Duration,
                        ImageUrl = c.ImageUrl,
                        IsFav = false, // ToDo User Favourites
                        InstructorName = c.Instructor.GetFullName(),
                        InstructorImageUrl = c.Instructor.ImageUrl,
                        Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                    })
                    .ToListAsync(cancellationToken);

                // Get categories for dropdown
                var categories = await _context.SubCategories
                    .Select(x => new SubCategoryDto
                    {
                        Id = x.Id,
                        Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
                        IconUrl = x.ImgIconUrl
                    })
                    .ToListAsync(cancellationToken);

             

                var response = new 
                {
                    TotalCount = totalCount,
                    Categories = categories,
              
                    AllCourses = courses
                };

                return new { Result = response };
            }
        }

    }
}