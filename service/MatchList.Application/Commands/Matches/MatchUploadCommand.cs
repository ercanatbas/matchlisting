using System.Collections.Generic;
using System.IO;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MatchList.Application.Commands.Matches
{
    public class MatchUploadCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
    }

    public class MatchUploadCommandValidation : AbstractValidator<MatchUploadCommand>
    {
        private static readonly List<string> Extensions = new() { ".xls", ".xlsx", ".json", ".xml" };

        public MatchUploadCommandValidation(ILogger<MatchUploadCommandValidation> logger)
        {
            RuleFor(match => match).NotNull().WithMessage("Your request cannot be null or empty.");
            RuleFor(match => match.File).NotNull().WithMessage("File not found")
                                        .Must(ValidateFileType).WithMessage("File not supported.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private static bool ValidateFileType(IFormFile file)
        {
            if (file == null)
                return false;

            var extension = Path.GetExtension(file.FileName);
            return Extensions.Contains(extension?.ToLower());
        }
    }
}