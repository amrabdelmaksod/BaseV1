﻿using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MethodologicalExplanations.Queries
{
    public class GetMethodlogicalExplanationByIdQuery : IRequest<object>
    {
        public int Id { get; set; }
    }

    public class GetMethodlogicalExplanationByIdQueryHandler : IRequestHandler<GetMethodlogicalExplanationByIdQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetMethodlogicalExplanationByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<object> Handle(GetMethodlogicalExplanationByIdQuery request, CancellationToken cancellationToken)
        {



            var explanation = await _context.MethodologicalExplanations
                .Include(e => e.ExplanationVideos).Include(a=>a.Instructor).Include(a=>a.ExplanationNotes)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (explanation == null)
            {
                // handle case where explanation is not found
                return null;
            }

            var explanationDto = new MethodlogicalExplanationDetailsDto
            {
                Id = explanation.Id,
                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? explanation.TitleAr : explanation.TitleEn,
                ImageUrl = explanation.ImageUrl,
                Description = explanation.Description,
                IsFav = false,
                Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{explanation.Duration} ساعات" : $"{explanation.Duration} hours",
                SubCategoryId = explanation.SubCategoryId,
                InstructorName = explanation.Instructor.GetFullName(),
                InstructorDescription = explanation.Instructor.Description,
                InstructorImgUrl = explanation.Instructor.ImageUrl,
                Facebook = explanation.Facebook,
                Whatsapp = explanation.Whatsapp,
                Telegram = explanation.Telegram,
                Twitter = explanation.Twitter,
                ExplanationVideoDtos = explanation.ExplanationVideos.Select(v => new ExplanationVideoDto
                {
                    Id = v.Id,
                    VideoUrl = v.VideoUrl,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? v.TitleAr : v.TitleEn,
                    Description = v.Description,
                }).ToList(),

                ExplanationNoteDtos = explanation.ExplanationNotes.Select(v=>new ExplanationNoteDto {
                Id = v.Id,
                Description = v.Description,
                IconUrl = v.IconUrl,
                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? v.TitleAr : v.TitleEn,

                }).ToList(),
            };

            return new { Result = explanationDto };
        }
    }

}