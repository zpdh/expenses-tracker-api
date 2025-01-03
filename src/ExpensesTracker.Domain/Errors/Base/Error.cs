﻿using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Domain.Errors.Base;

public record Error(string ErrorMessage)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error NullArgument = new("The specified value is null.");
    public static readonly Error ConditionNotMet = new("The specified condition was not met.");

    public static implicit operator Result(Error error)
    {
        return Result.Failure(error);
    }
}