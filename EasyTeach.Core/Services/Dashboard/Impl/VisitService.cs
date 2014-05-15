using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;
        private readonly EntityValidator _entityValidator;
        private readonly IVisitDtoMapper _visitDtoMapper;

        public VisitService(
            IVisitRepository visitRepository,
            ILessonRepository lessonRepository,
            IUserRepository userRepository,
            EntityValidator entityValidator,
            IVisitDtoMapper visitDtoMapper)
        {
            if (visitRepository == null)
            {
                throw new ArgumentNullException("visitRepository");
            }

            if (lessonRepository == null)
            {
                throw new ArgumentNullException("lessonRepository");
            }

            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            if (visitDtoMapper == null)
            {
                throw new ArgumentNullException("visitDtoMapper");
            }

            _visitRepository = visitRepository;
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
            _entityValidator = entityValidator;
            _visitDtoMapper = visitDtoMapper;
        }

        public IQueryable<IVisitModel> GetGroupVisits(int groupId)
        {
            int[] groupMateIds = _userRepository.GetUsers().Where(u => u.GroupId == groupId).Select(u => u.UserId).ToArray();
            int[] lessonIds =_lessonRepository.GetLessons().Where(l => l.GroupId == groupId).Select(l => l.LessonId).ToArray();

            IVisitDto[] existingVisits = _visitRepository.GetAll()
                .Where(v => groupMateIds.Contains(v.UserId) || lessonIds.Contains(v.LessonId))
                .ToArray();

            var allPossibleVisits = new HashSet<Tuple<int, int>>();
            foreach (int groupMateId in groupMateIds)
            {
                foreach (int lessonId in lessonIds)
                {
                    allPossibleVisits.Add(new Tuple<int, int>(groupMateId, lessonId));
                }
            }

            var result = new List<IVisitModel>(allPossibleVisits.Count);
            foreach (var existingVisit in existingVisits)
            {
                var visit = new Visit
                {
                    Lesson = new Lesson { LessonId = existingVisit.LessonId },
                    Note = existingVisit.Note,
                    Status = existingVisit.Status,
                    Visitor = new User { UserId = existingVisit.UserId }
                };

                result.Add(visit);
                allPossibleVisits.Remove(new Tuple<int, int>(existingVisit.UserId, existingVisit.LessonId));
            }

            foreach (Tuple<int, int> possibleVisit in allPossibleVisits)
            {
                var visit = new Visit
                {
                    Lesson = new Lesson { LessonId = possibleVisit.Item2 },
                    Note = null,
                    Status = VisitStatus.Skipped,
                    Visitor = new User { UserId = possibleVisit.Item1 }
                };

                result.Add(visit);
            }

            return result.AsQueryable();
        }

        public IQueryable<IVisitModel> GetGroupVisitsAvailableForStudent(IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            if (!principal.Identity.IsAuthenticated)
            {
                throw new ArgumentException("Identity of principal wasn't authenticated");
            }

            IUserDto user = _userRepository.GetUserByEmail(principal.Identity.Name).Result;
            if (user.GroupId == null)
            {
                throw new GroupNotFoundException(String.Format("User '{0}' doesn't belong to any group", principal.Identity.Name));
            }

            return GetGroupVisits(user.GroupId.Value);
        }

        public async Task UpdateVisitAsync(IVisitModel visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(visit);
            if (result.IsValid == false)
            {
                throw new InvalidVisitException(result.ValidationResults);
            }

            IUserDto user = await _userRepository.GetUserById(visit.Visitor.UserId);
            if (user == null)
            {
                throw new InvalidVisitException(new[] {new ValidationResult("Visitor does not exists") });
            }

            ILessonDto lesson = await _lessonRepository.GetLessonByIdAsync(visit.Lesson.LessonId);
            if (lesson == null)
            {
                throw new InvalidVisitException(new[] { new ValidationResult("Lesson does not exists") });
            }

            IVisitDto visitDto = await _visitRepository.GetVisitAsync(user.UserId, lesson.LessonId);
            if (visitDto == null)
            {
                await _visitRepository.CreateVisitAsync(_visitDtoMapper.Map(visit));
            }
            else
            {
                await _visitRepository.UpdateVisitAsync(_visitDtoMapper.Map(visit));
            }
        }
    }
}