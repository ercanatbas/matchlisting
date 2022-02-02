using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatchList.Application.Exceptions;
using MatchList.Application.Queries.Matches;
using MatchList.Domain.Matches;
using MatchList.Domain.Matches.Enums;
using MatchList.Infrastructure.Repositories;
using MatchList.Infrastructure.Repositories.UnitOfWork;
using MatchList.Infrastructure.Serializing;
using MatchList.Infrastructure.Services.Map;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchUploadHandler : IRequestHandler<MatchUploadCommand, bool>
    {
        private readonly IUnitOfWork      _uow;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapperService   _mapper;
        private readonly IMediator        _mediator;

        public MatchUploadHandler(IUnitOfWork      uow,
                                  IMatchRepository matchRepository,
                                  IMapperService   mapper,
                                  IMediator        mediator)
        {
            _uow             = uow;
            _matchRepository = matchRepository;
            _mapper          = mapper;
            _mediator        = mediator;
        }

        public async Task<bool> Handle(MatchUploadCommand command, CancellationToken cancellationToken)
        {
            var fileExtension = Path.GetExtension(command.File.FileName)?.ToLower().Replace(".", string.Empty);
            Enum.TryParse(fileExtension, true, out MatchFileFormat matchFileFormat);

            var matchModels = matchFileFormat switch
            {
                MatchFileFormat.Xls or MatchFileFormat.Xlsx => new SerializeToXls().Deserialize<List<MatchModel>>(command.File),
                MatchFileFormat.Json => new SerializeToJson().Deserialize<List<MatchModel>>(command.File),
                MatchFileFormat.Xml => new SerializeToXml().Deserialize<XmlMatchModel>(command.File)?.MatchModels,
                _ => throw new ApplicationCustomException("File format was not found")
            };

            if (matchModels == null || !matchModels.Any())
            {
                throw new ApplicationCustomException("Content not found");
            }

            matchModels = matchModels.Where(m => m.EventId > 0 && m.EventType is not EventType.Unknown && m.EventTime != DateTime.MinValue).Distinct().ToList();

            matchModels = await _mediator.Send(new MatchAlreadyRecordUpdateCommand(matchModels),  cancellationToken);
            matchModels = await _mediator.Send(new MatchDuplicateRecordParseCommand(matchModels), cancellationToken);

            if (matchModels.Any())
            {
                var matches = _mapper.Map<List<Match>>(matchModels);
                await _matchRepository.InsertRangeAsync(matches);
            }

            var result = await _uow.SaveAsync(cancellationToken);
            if (result)
            {
                return true;
            }

            throw new ApplicationCustomException("No change found.");
        }
    }
}