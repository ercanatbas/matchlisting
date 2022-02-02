using System.Collections.Generic;
using MatchList.Application.Queries.Matches;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchDuplicateRecordParseCommand : IRequest<List<MatchModel>>
    {
        public List<MatchModel> MatchModels { get; set; }

        public MatchDuplicateRecordParseCommand(List<MatchModel> matchModels)
        {
            MatchModels = matchModels;
        }
    }
}