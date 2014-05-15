using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Services.Quiz.Exceptions;

namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class QuestionViewModel
    {
        public string QuestionText { get; set; }

        public string QuestionType { get; set; }

        public IEnumerable<QuestionItemViewModel> QuestionItems { get; set; }

        public virtual Question ToQuestion()
        {
            QuestionType type;
            if (Enum.TryParse(QuestionType, true, out type) == false)
            {
                throw new InvalidAddQuestionException(
                    new List<ValidationResult>
                    {
                        new ValidationResult(
                            "There is no such Question Type. Should be: Select, MultiSelect or Text.",
                            new[] { "QuestionType" })
                    });
            }
            return new Question
                   {
                       QuestionText = QuestionText,
                       QuestionType = type,
                       QuestionItems =
                           QuestionItems.Select(
                               x => new QuestionItemModel { IsSolution = x.IsSolution, Text = x.Text })
                           .ToList()
                   };
        }
    }

    public class QuestionItemViewModel
    {
        public string Text;

        public bool IsSolution;
    }
}