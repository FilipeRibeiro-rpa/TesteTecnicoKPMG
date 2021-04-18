using Points.Domain.Exceptions;
using Points.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Points.Domain.AggregatesModel.GameResultAggregate
{
    public class GameResult: Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public long PlayerId { get; private set; }
        public long GameId { get; private set; }
        public long Win { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime TimeStamp { get; private set; }

        protected GameResult() {    }
        public GameResult(string identityguid, long playerid, long gameid, long win, DateTime timestamp, DateTime createdate) : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityguid) ? identityguid : throw new GameResultDomainException("Invalid Identity");
            PlayerId = playerid <= 0 ? throw new GameResultDomainException("Invalid PlayerId") : playerid;
            GameId = gameid <= 0 ? throw new GameResultDomainException("Invalid GameId") : gameid;
            Win = win;
            TimeStamp = timestamp.IsDaylightSavingTime() ? throw new GameResultDomainException("Invalid Date") : timestamp;
            CreateDate = createdate;
        }
    }

    public class Leaderboard
    {
        public long PlayerId { get; private set; }
        public long Balance { get; private set; }

        protected Leaderboard() { }

        public Leaderboard(long player, long balance)
        {
            PlayerId = player;
            Balance = balance;
        }
    }
}
