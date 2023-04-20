using Hedaya.Application.Courses.Models;
using System.Globalization;
using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using Hedaya.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.TrainingPrograms.Queries
{
    public class GetProgramByIdQuery : IRequest<object>
    {
        public int ProgramId { get; set; }
        public string UserId { get; set; }


        public class GetProgramByIdQueryHandler : IRequestHandler<GetProgramByIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetProgramByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
            {


                var traineeId = await _context.Trainees
                 .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                 .Select(a => a.Id)
                 .FirstOrDefaultAsync(cancellationToken);

                var program = await _context.TrainingPrograms
                    .Include(p => p.SubCategory)
                    .Include(p => p.TrainingProgramNotes)
                    .Include(p => p.Courses)
                    .FirstOrDefaultAsync(p => p.Id == request.ProgramId);

                if (program == null)
                {
                    throw new NotFoundException(nameof(TrainingProgram));
                }

                var programDto = new ProgramDetailsDto
                {
                    ImgUrl = program.ImgUrl,
                    Title = CultureInfo.CurrentCulture.Name == "ar" ? program.TitleAr : program.TitleEn,
                    SubCategoryName = CultureInfo.CurrentCulture.Name == "ar" ? program.SubCategory.NameAr : program.SubCategory.NameEn,
                    StartDate = program.StartDate,
                    EndDate = program.EndDate,
                    IsFav = _context.TraineeFavouritePrograms.Any(f => f.TrainingProgramId == program.Id && f.TraineeId == traineeId),
                    IsEnrolled = _context.Enrollments.Any(f => f.TrainingProgramId == program.Id && f.TraineeId == traineeId),
                    TrainingProgramNotes = program.TrainingProgramNotes.OrderBy(a=>a.SortIndex).Select(n => new TrainingProgramNoteDto
                    {
                        Id = n.Id,
                        Note = CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="ar"? n.TextAr : n.TextEn,
                        SortIndex = n.SortIndex
                    }).ToList(),
                    Courses = program.Courses.Select(c => new CoursesLiDto
                    {
                        Id = c.Id,
                        Title = CultureInfo.CurrentCulture.Name == "ar" ? c.TitleAr : c.TitleEn,             
                    }).ToList()
                };

                var relatedPrograms = await _context.TrainingPrograms
                    .Where(p => p.SubCategoryId == program.SubCategoryId && p.Id != program.Id)
                    .Take(10)
                    .Select(p => new TrainingProgramDto
                    {
                        Id = p.Id,
                        Title = CultureInfo.CurrentCulture.Name == "ar" ? p.TitleAr : p.TitleEn,
                        ImgUrl = p.ImgUrl,
                        StartDate = p.StartDate,
                        SubCategoryName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? p.SubCategory.NameAr : p.SubCategory.NameEn                        
                    })
                    .ToListAsync();

                programDto.RelatedPrograms = relatedPrograms;

                return new { Result = programDto } ;
            }
        }

    }

}
