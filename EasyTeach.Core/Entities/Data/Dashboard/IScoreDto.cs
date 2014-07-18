using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface IScoreDto
    {
        int ScoreId { get; set; }

        int Score { get; }

        IUserIdentityModel AssignedTo { get; }

        IUserIdentityModel AssignedBy { get; }

        IVariantProgressModel Task { get; }

        IVisitModel Visit { get; }
    }
}
