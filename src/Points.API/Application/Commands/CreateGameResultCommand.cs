using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Points.API.Application.Commands
{
    [DataContract]
    public class CreateGameResultCommand : IRequest<GameResultDTO>
    {
        [DataMember]
        public long PlayerId { get; set; }
        [DataMember]
        public long GameId { get; set; }
        [DataMember]
        public long Win { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
    }

    public class GameResultDTO
    {
        public string IdentityGuid { get; set; }
    }
}
