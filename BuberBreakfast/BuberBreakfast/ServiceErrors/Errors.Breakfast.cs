using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "breakfast.not-food",
            description: "Breakfast not found."
        );
        public static Error InvalidName => Error.Validation(
            code: "breakfast.invalid-name",
            description: $"Name must be between {Models.Breakfast.MinNameLength} and {Models.Breakfast.MaxNameLength} characters long."
        );
        public static Error InvalidDescription => Error.Validation(
            code: "breakfast.invalid-description",
            description: $"Description must be between {Models.Breakfast.MinDescriptionLength} and {Models.Breakfast.MaxDescriptionLength} characters long."
        );
    }
}