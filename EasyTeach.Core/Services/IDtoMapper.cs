using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services
{
    public interface IDtoMapper
    {
        IUserDto Map(IUserModel userModel);
    }
}