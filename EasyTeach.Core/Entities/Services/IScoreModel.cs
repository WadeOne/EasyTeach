﻿namespace EasyTeach.Core.Entities.Services
{
    public interface IScoreModel
    {
        int Score { get; }

        IUserIdentityModel AssignedTo { get; }

        IUserIdentityModel AssignedBy { get; }

        IVariantProgressModel Task { get; }

        IVisitModel Visit { get; }
    }
}