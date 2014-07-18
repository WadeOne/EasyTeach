using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Data.Entities
{
    public sealed class ScoreDto : IScoreDto
    {
        public int ScoreId { get; set; }
        public int Score { get; private set; }
        public IUserIdentityModel AssignedTo { get; private set; }
        public IUserIdentityModel AssignedBy { get; private set; }
        public IVariantProgressModel Task { get; private set; }
        public IVisitModel Visit { get; private set; }
    }
}
