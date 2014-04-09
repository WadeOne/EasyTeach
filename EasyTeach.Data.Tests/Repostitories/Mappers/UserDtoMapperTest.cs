using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Data.Repostitories.Mappers;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Data.Tests.Repostitories.Mappers
{
    public sealed class UserDtoMapperTest
    {
        private readonly UserDtoMapper _mapper = new UserDtoMapper();

        [Fact]
        public void Map_UserModel_MappedCorrectly()
        {
            var userModel = A.Fake<IUserModel>();
            A.CallTo(() => userModel.FirstName).Returns("FirstName");
            A.CallTo(() => userModel.LastName).Returns("LastName");
            A.CallTo(() => userModel.Email).Returns("test@test.com");
            A.CallTo(() => userModel.Group).Returns(new Group{GroupId = 1, GroupNumber = 2, Year = 2009});
            A.CallTo(() => userModel.UserType).Returns(UserType.Student);

            IUserDto userDto = _mapper.Map(userModel);

            Assert.Equal(userDto.FirstName, userModel.FirstName);
            Assert.Equal(userDto.LastName, userModel.LastName);
            Assert.Equal(userDto.Email, userModel.Email);
            Assert.Equal(userDto.UserType, userModel.UserType);
            Assert.Equal(userDto.Group.GroupId, userModel.Group.GroupId);
            Assert.Equal(userDto.Group.GroupNumber, userModel.Group.GroupNumber);
            Assert.Equal(userDto.Group.Year, userModel.Group.Year);
        }

        [Fact]
        public void Map_UserIdentityModel_MappedCorrectly()
        {
            var userModel = A.Fake<IUserIdentityModel>();
            A.CallTo(() => userModel.Email).Returns("test@test.com");
            A.CallTo(() => userModel.UserId).Returns(42);

            IUserDto userDto = _mapper.Map(userModel);
            Assert.Equal(userDto.Email, userModel.Email);
            Assert.Equal(userDto.UserId, userModel.UserId);
        }
    }
}
