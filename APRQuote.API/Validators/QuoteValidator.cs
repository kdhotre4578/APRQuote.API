using System.Text;
using FluentValidation;
using APRQuote.Core.Models;

namespace APRQuote.API.Validators
{
    public class QuoteValidator :  AbstractValidator<AprQuote>
    {
        public QuoteValidator()
        {
            RuleFor(m => m.Make)
                .NotEmpty()
                .WithMessage("Vehicle make cannot be empty")
                .MaximumLength(30)
                .WithMessage("Vehicle make text limit exceeded");

            RuleFor(m => m.VehicleType)
                .NotEmpty()
                .WithMessage("Vehicle type cannot be empty")
                .MaximumLength(20)
                .WithMessage("Vehicle type text limit exceeded");

            RuleFor(m => m.QuoteType)
                .NotEmpty()
                .WithMessage("Quote type cannot be empty")
                .MaximumLength(3)
                .WithMessage("Quote type limit exceeded");

            RuleFor(m => m.ZeroThreeMonths)
                .NotEmpty()
                .WithMessage("Invalid zero-three months apr");

            RuleFor(m => m.ThreeSixMonths)
                .NotEmpty()
                .WithMessage("Invalid three-six months apr");

            RuleFor(m => m.SixTwelveMonths)
                .NotEmpty()
                .WithMessage("Invalid six-twelve months apr");

            RuleFor(m => m.TwelvePlusMonths)
                .NotEmpty()
                .WithMessage("Invalid twelve plus months apr");
        }

        public string GetErrorDetails(FluentValidation.Results.ValidationResult validationResult)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < validationResult.Errors.Count; i++)
            {
                if (i == validationResult.Errors.Count - 1)
                {
                    sb.Append((i > 0 ? " and " : "") + validationResult.Errors[i].ErrorMessage);
                }
                else
                {
                    sb.Append(validationResult.Errors[i].ErrorMessage
                        + (i >= validationResult.Errors.Count - 2 ? "" : ", "));
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
