using FluentValidation;

namespace HotelManager.MVVM.Models.DataContract;

public class PeopleValidator : AbstractValidator<People>
{
    private const int _minAge = 0;
    private const int _maxAge = 200;

    private const string Regex = "[a-z]*";
    
    public PeopleValidator()
    {
        RuleFor(people => people.Age).InclusiveBetween(_minAge, _maxAge)
            .WithMessage($"Возраст должен быть от {_minAge} до {_maxAge}");
        RuleFor(people => people.FullName).Matches(Regex)
            .WithMessage($"ФИО должно состоять из русских букв!");
    }
}