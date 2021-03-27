using FluentValidation;
using System.Linq;

namespace Football.Application.Matchs.Commands
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator()
        {
            RuleFor(v => v.AwayManager).NotEqual(0);
            RuleFor(v => v.HouseManager).NotEqual(0);
            RuleFor(v => v.HouseTeam).Must(t => t.Count() > 1).WithMessage("Players amount must be higher than 1");
            RuleFor(v => v.AwayTeam).Must(t => t.Count() > 1).WithMessage("Players amount must be higher than 1");
        }
    }
}
