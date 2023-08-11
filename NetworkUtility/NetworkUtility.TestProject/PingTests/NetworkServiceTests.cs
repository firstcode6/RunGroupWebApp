using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System.Net;

namespace NetworkUtility.TestProject.PingTests
{
    
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dns;
        public NetworkServiceTests()
        {
            //Dependencies
            _dns = A.Fake<IDNS>();

            //SUT (System Under Test) // Arrange - oрганизовать
            _pingService = new NetworkService(_dns);
        }

        [Fact] //Test attribute
        public void NetworkService_SendPing_ReturnString() 
        {
            // Arrange - Go get your variables, whatever you need, you classes, go get functions
            A.CallTo(() => _dns.SendDNS()).Returns(true);

            // Act - Execute this function(SendPing())
            var result = _pingService.SendPing();

            // Assert - Whatever is returned is it what you want?
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        //////////////////////////////////////////////////////////////////////////////////

        [Theory] // for passing variales or some data
        [InlineData(1, 2, 3)]
        [InlineData(4, 6, 10)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            // Arrange - oрганизовать

            // Act
            var result = _pingService.PingTimeout(a, b);

            // Assert - утверждать
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(3);
            result.Should().NotBeInRange(-1000, 0);
        }

        //////////////////////////////////////////////////////////////////////////////////
        [Fact] 
        public void NetworkService_LastPingDate_ReturnDate()
        {
            // Arrange 

            // Act
            var result = _pingService.LastPingDate();

            // Assert 
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        //////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            // Arrange
            var expected = new AggregateGroupVM()
            {
                Id = 1,
                Name = "First"
            };

            // Act
            var result = _pingService.GetPingOptions();

            // Assert WARNING: "Be" careful
            result.Should().BeOfType<AggregateGroupVM>();
            result.Should().BeEquivalentTo(expected);
            result.Id.Should().Be(1);
        }

        //////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void NetworkService_MostRecentPings_ReturnIEnumerableObject()
        {
            // Arrange
            var expected = new AggregateGroupVM()
            {
                Id = 1,
                Name = "First"
            };

            // Act
            var result = _pingService.MostRecentPings();

            // Assert WARNING: "Be" careful
            result.Should().BeOfType<AggregateGroupVM[]>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.Name == "Third");
        }

        //////////////////////////////////////////////////////////////////////////////////



    }
}
