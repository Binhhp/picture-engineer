using FluentValidation;
using PictureEngineer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Validator
{
    public class PDFExtractPageValidator : AbstractValidator<PDFExtractPagesDto>
    {
        public PDFExtractPageValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty().NotNull();
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
        }
    }
    public class PDFSplitValidator : AbstractValidator<PDFSplitDto>
    {
        public PDFSplitValidator()
        {
            RuleFor(x => x.StartPage).NotEmpty().NotNull();
            RuleFor(x => x.EndPage).NotEmpty().NotNull();
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
        }
    }
    public class PDFTranslateValidator : AbstractValidator<PDFTranslateDto>
    {
        public PDFTranslateValidator()
        {
            RuleFor(x => x.SourceLanguage).NotEmpty().NotNull();
            RuleFor(x => x.TextLanguage).NotEmpty().NotNull();
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
        }
    }
    public class PDFValidator : AbstractValidator<PDFConvertDto>
    {
        public PDFValidator()
        {
            RuleFor(x => x.FileFormatConvert).NotEmpty().NotNull();
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
        }
    }
    public class DocxValidator : AbstractValidator<DocxToPdf>
    {
        public DocxValidator()
        {
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
        }
    }
}
