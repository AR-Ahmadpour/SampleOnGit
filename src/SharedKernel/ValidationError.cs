namespace SharedKernel;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "Validation.General",
            "One or more validation errors occurred",
            ErrorType.Validation)
    {
        Errors = errors.Select(x => x.Description).ToList(); 
    }

    public List<string> Errors { get; }
    
    
}
