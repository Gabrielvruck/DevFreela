using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new PaginationResult<Project>
            {
                Data = new List<Project>
                {
                  new ("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000),
                  new ("Nome Do Teste 2", "Descricao De Teste 2", 1, 2, 20000),
                  new ("Nome Do Teste 3", "Descricao De Teste 3", 1, 2, 30000)
                },
                ItemsCount = 3,
                Page = 1,
                PageSize = 10,
                TotalPages = 10
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync("", 1, It.IsAny<CancellationToken>()).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery();
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(projectViewModelList.Data);
            Assert.NotEmpty(projectViewModelList.Data);
            Assert.Equal(projects.Data.Count, projectViewModelList.Data.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync("", 1, It.IsAny<CancellationToken>()).Result, Times.Once);
        }
    }
}
