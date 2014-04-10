using System;

namespace EasyTeach.Core.Entities.Data
{
    public interface IUserTokenDto
    {
        int UserTokenId { get; }

        string Puprose { get; }

        string Token { get; }

        int UserId { get; }

        DateTime Created { get; }
    }
}