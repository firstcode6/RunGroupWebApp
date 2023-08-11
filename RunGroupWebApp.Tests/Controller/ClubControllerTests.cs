using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Controllers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Tests.Controller
{
    public class ClubControllerTests
    {
        private IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClubController _clubController;

        public ClubControllerTests()
        {
            // Dependencies:
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT (System Under Test) // Arrange - oрганизовать
            _clubController = new ClubController(_clubRepository, _photoService, _httpContextAccessor);
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            // Arrange - Go get your variables, whatever you need, you classes, go get functions
            // instead of IEnumerable<Club> clubs = await _clubRepository.GetAll(); in ClubController.cs
            var clubs = A.Fake<IEnumerable<Club>>(); 
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);

            // Act - Execute this function(Index())
            var result = _clubController.Index();

            // Assert - Whatever is returned is it what you want?
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var result = _clubController.DetailClub(id  );

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
