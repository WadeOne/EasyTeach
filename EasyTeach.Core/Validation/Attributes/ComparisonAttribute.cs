using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EasyTeach.Core.Validation.Attributes
{
    

    [AttributeUsage(AttributeTargets.Property)]
    public class ComparisonAttribute : ValidationAttribute
    {
        public enum ComparisonCondition
        {
            Equal = 0,
            Greater = 1,
            Less = 2,
            GreaterOrEqual = 3,
            LessOrEqual = 4,
            NotEqual = 5
        }

        private readonly Dictionary<ComparisonCondition, Func<object, object, bool>> _comparisonFunctions;

        private readonly string _otherProperty;
        private readonly ComparisonCondition _comparisonCondition;
        private IComparer _comparer;

        public ComparisonAttribute(string otherProperty, ComparisonCondition comparisonCondition)
        {
            _otherProperty = otherProperty;
            _comparisonCondition = comparisonCondition;

            _comparisonFunctions = new Dictionary<ComparisonCondition, Func<object, object, bool>>
            {
                {ComparisonCondition.Equal, (x, y) => _comparer.Compare(x, y) == 0},
                {ComparisonCondition.Greater, (x, y) => _comparer.Compare(x, y) > 0},
                {ComparisonCondition.Less, (x, y) => _comparer.Compare(x, y) < 0}
            };

            _comparisonFunctions.Add(ComparisonCondition.GreaterOrEqual,
                (x, y) =>
                    _comparisonFunctions[ComparisonCondition.Greater](x, y) ||
                    _comparisonFunctions[ComparisonCondition.Equal](x, y));

            _comparisonFunctions.Add(ComparisonCondition.LessOrEqual,
                (x, y) =>
                    _comparisonFunctions[ComparisonCondition.Less](x, y) ||
                    _comparisonFunctions[ComparisonCondition.Equal](x, y));

            _comparisonFunctions.Add(ComparisonCondition.NotEqual, (x, y) => _comparisonFunctions[ComparisonCondition.Equal](x, y) == false);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_comparer == null)
            {
                _comparer = Comparer.Default;
            }
            PropertyInfo property = validationContext.ObjectType.GetProperty(_otherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", _otherProperty));
            }

            object otherPropertyValue = property.GetValue(validationContext.ObjectInstance);
            if (_comparisonFunctions[_comparisonCondition](value, otherPropertyValue) == false)
            {
                return new ValidationResult(ErrorMessage);
            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Field is invalid";
        }
    }
}
