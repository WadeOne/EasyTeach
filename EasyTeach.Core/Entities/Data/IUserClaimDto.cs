namespace EasyTeach.Core.Entities.Data
{
    public interface IUserClaimDto
    {
        int UserClaimId { get; }

        string Value { get; }

        string Type { get; }

        string ValueType { get; }
    }
}