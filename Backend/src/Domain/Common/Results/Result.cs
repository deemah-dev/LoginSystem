using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Common.Results;

public readonly record struct Success;
public readonly record struct Created;
public readonly record struct Deleted;
public readonly record struct Updated;

public static class Result
{
    public static Success Success => default;
    public static Created Created => default;
    public static Deleted Deleted => default;
    public static Updated Updated => default;
}

public sealed class Result<TValue> : IResult<TValue>
{
    private readonly TValue? value = default;
    private readonly List<Error>? errors = null;

    public TValue Value => IsSuccess ? value! : default!;
    public List<Error> Errors => IsError ? errors! : [];
    public Error TopError => (errors?.Count > 0) ? errors[0] : default;

    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;


    private Result(Error error)
    {
        this.errors = [error];
        IsSuccess = false;
    }
    private Result(List<Error> errors)
    {
        if (errors is null || errors.Count == 0)
            throw new ArgumentException(
                "Cannot create and Error<TValue> from an empty collection of errors. Provide at least one error.",
                nameof(errors)
            );

        this.errors = errors;
        IsSuccess = false;
    }
    private Result(TValue value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        this.value = value;
        this.errors = [];
        IsSuccess = true;
    }

    public static implicit operator Result<TValue>(Error error)
    => new(error);
    public static implicit operator Result<TValue>(List<Error> errors)
    => new(errors);
    public static implicit operator Result<TValue>(TValue value)
    => new(value);

    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
    => IsSuccess ? onValue(Value) : onError(Errors);

    [JsonConstructor]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("for serializer only", true)]
    public Result(TValue? value, List<Error>? errors, bool isSuccess)
    {
        if (isSuccess)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            this.errors = [];
            this.IsSuccess = true;
        }
        else
        {
            if (errors is null || errors.Count == 0)
                throw new ArgumentException(
                    "Cannot create and Error<TValue> from an empty collection of errors. Provide at least one error.",
                    nameof(errors)
                );

            this.errors = errors;
            IsSuccess = false;
        }
    }
}