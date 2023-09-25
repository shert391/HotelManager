using FluentValidation;

namespace HotelManager.MVVM.Models.DataContract;

public class PeopleValidator : AbstractValidator<People>
{
    private const int _minAge = 0;
    private const int _maxAge = 200;

    public PeopleValidator()
    {
        RuleFor(people => people.Age).InclusiveBetween(_minAge, _maxAge).NotNull().NotEmpty();
        RuleFor(people => people.SeriesPassport).Matches("^[+0-9]+$").NotEmpty().NotNull();
        RuleFor(people => people.NumberPassport).Matches("^[+0-9]+$").NotNull().NotEmpty();
        RuleFor(people => people.PhoneNumber).Matches("^[+0-9]+$").NotNull().NotEmpty();
        RuleFor(people => people.FullName).Matches("^[а-яА-Я ]+$").NotEmpty().NotNull();
        RuleFor(people => people.ResidenceAddress).NotNull().NotEmpty();
    }
}