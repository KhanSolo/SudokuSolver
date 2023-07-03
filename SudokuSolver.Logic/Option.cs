namespace SudokuSolver.Logic;

public readonly struct Option<TResult, TError>
{
    public Option(TResult result) { Result = result; Error = default; }
    public Option(TError error) { Result = default; Error = error; }

    public TResult Result { get; }
    public TError Error { get; }
}

public record Error(string Message);
