using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface IGroupModel
    {
        int GroupId { get; }

        /// <summary>
        /// Group number
        /// </summary>
        int GroupNumber { get; }

        /// <summary>
        /// University entering year
        /// </summary>
        int Year { get; }

        ICollection<IUserModel> Students { get; }
    }
}