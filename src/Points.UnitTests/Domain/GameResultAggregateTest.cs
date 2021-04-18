using Points.Domain.AggregatesModel.GameResultAggregate;
using Points.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Points.UnitTests.Domain
{
    public class GameResultAggregateTest
    {
        public GameResultAggregateTest() { }

        [Fact]
        public void Create_gameresult_item_success()
        {
            //Arrange
            var identify = new Guid().ToString();
            var player = 1;
            var game = 2;
            var win = 3;
            var timestamp = DateTime.UtcNow;
            var created = DateTime.UtcNow;

            //Act
            var FakeGameResultItem = new GameResult(identify, player, game, win, timestamp, created);

            //Assert
            Assert.NotNull(FakeGameResultItem);
        }

        [Fact]
        public void Create_gameresult_item_fail()
        {
            //Arrange    
            var identify = new Guid().ToString();
            var player = -1;
            var game = 2;
            var win = 0;
            var timestamp = DateTime.Now;
            var created = DateTime.UtcNow;

            //Act - Assert
            Assert.Throws<GameResultDomainException>(() => new GameResult(identify, player, game, win, timestamp, created));
        }
    }
}
