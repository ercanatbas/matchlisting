using System.Collections.Generic;
using MatchList.Application.Queries.Matches;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchAlreadyRecordUpdateCommand : IRequest<List<MatchModel>>
    {
        public List<MatchModel> MatchModels { get; set; }

        public MatchAlreadyRecordUpdateCommand(List<MatchModel> matchModels)
        {
            MatchModels = matchModels;
        }
    }
}