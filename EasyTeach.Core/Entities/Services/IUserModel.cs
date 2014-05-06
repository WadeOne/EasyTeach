namespace EasyTeach.Core.Entities.Services
{
    public interface IUserModel : IUserIdentityModel
    {
        string FirstName { get; }

        string LastName { get; }

        IGroupModel Group { get; }
    }
}
