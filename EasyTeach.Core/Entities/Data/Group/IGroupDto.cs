namespace EasyTeach.Core.Entities.Data.Group
{
    public interface IGroupDto
    {
        int GroupId { get; }

        int GroupNumber { get; }

        int Year { get; }

        string ContactEmail { get; }

        string ContactPhone { get; }

        string ContactName { get; }
    }
}