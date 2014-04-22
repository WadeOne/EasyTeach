using EasyTeach.Data.Repostitories.Mappers;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories.Mappers
{
    public class QuizDtoMapperTest
    {
        private readonly QuizDtoMapper _mapper;

        public QuizDtoMapperTest()
        {
            _mapper = new QuizDtoMapper();
        }

        [Fact]
        public void Map_FromModelToDto_Mapped()
        {
            
        }
    }
}
