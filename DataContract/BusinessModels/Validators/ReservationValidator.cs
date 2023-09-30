using FluentValidation;

namespace DataContract.BusinessModels.Validators;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(reservation => reservation.EndData).NotNull().NotEmpty().Must(IsDataValid).WithMessage("Дата выселения введена неправильно!");
        RuleFor(reservation => reservation.Peoples).NotNull().NotEmpty();
    }
    private bool IsDataValid(DateTime date) => date > DateTime.Now;
}