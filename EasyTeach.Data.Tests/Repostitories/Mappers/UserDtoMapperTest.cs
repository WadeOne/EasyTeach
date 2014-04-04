using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Data.Repostitories.Mappers;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Data.Tests.Repostitories.Mappers
{
    public class UserDtoMapperTest
    {
        private readonly UserDtoMapper mapper;

        public UserDtoMapperTest()
        {
            mapper = new UserDtoMapper();
        }

        [Fact]
        public void Map_UserModel_MappedCorrectly()
        {
            var userModel = A.Fake<IUserModel>();
            A.CallTo(() => userModel.FirstName).Returns("FirstName");
            A.CallTo(() => userModel.LastName).Returns("LastName");
            A.CallTo(() => userModel.Email).Returns("test@test.com");
            A.CallTo(() => userModel.Group).Returns(new Group{GroupId = 1, GroupNumber = 2, Year = 2009});
            A.CallTo(() => userModel.UserType).Returns(UserType.Student);
            A.CallTo(() => userModel.EmailIsValidated).Returns(true);

            var userDto = mapper.Map(userModel);

            Assert.True(userDto.FirstName.Equals(userModel.FirstName));
            Assert.True(userDto.LastName.Equals(userModel.LastName));
            Assert.True(userDto.Email.Equals(userModel.Email));
            Assert.True(userDto.UserType.Equals(userModel.UserType));
            Assert.True(userDto.EmailIsValidated.Equals(userModel.EmailIsValidated));
            Assert.True(userDto.Group.GroupId.Equals(userModel.Group.GroupId));
            Assert.True(userDto.Group.GroupNumber.Equals(userModel.Group.GroupNumber));
            Assert.True(userDto.Group.Year.Equals(userModel.Group.Year));
        }
    }
}
