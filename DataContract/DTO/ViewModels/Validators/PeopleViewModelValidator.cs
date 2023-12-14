using DataContract.GlobalConstants;
using FluentValidation;

namespace DataContract.DTO.ViewModels.Validators;

public class PeopleViewModelValidator : AbstractValidator<PeopleViewModel>
{
    public PeopleViewModelValidator()
    {
        RuleFor(people => people.Age).InclusiveBetween(PeopleConstants.MinAge, PeopleConstants.MaxAge).NotNull().NotEmpty();
        RuleFor(people => people.SeriesPassport).Matches("^[+0-9]+$").NotEmpty().NotNull();
        RuleFor(people => people.NumberPassport).Matches("^[+0-9]+$").NotNull().NotEmpty();
        RuleFor(people => people.PhoneNumber).Matches("^[+0-9]+$").NotNull().NotEmpty();
        RuleFor(people => people.FullName).Matches("^[а-яА-Яa-zA-Z ]+$").NotEmpty().NotNull();
        RuleFor(people => people.ResidenceAddress).NotNull().NotEmpty();
    }
}