using DataContract.GlobalConstants;
using FluentValidation;

namespace DataContract.BusinessModels.Validators;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.Number)
            .GreaterThan(1)
            .NotNull()
            .NotEmpty();

        RuleFor(room => room.Price)
            .InclusiveBetween(RoomConstants.MinPrice, RoomConstants.MaxPrice)
            .NotNull()
            .NotEmpty();

        RuleFor(room => room.Type).IsInEnum();
    }
}