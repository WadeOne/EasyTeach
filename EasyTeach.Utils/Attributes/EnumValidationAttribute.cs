using System;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class EnumValidationAttribute : ValidationAttribute
    {
        public EnumValidationAttribute(Type enumType)
            : base("enumeration")
        {
            EnumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (EnumType == null)
            {
                throw new InvalidOperationException("Type cannot be null");
            }
            if (EnumType.IsEnum == false)
            {
                throw new InvalidOperationException("Type must be an enum");
            }
            if (Enum.IsDefined(EnumType, value) == false)
            {
                return false;
            }
            return true;
        }

        public Type EnumType { get; set; }
    }
}
