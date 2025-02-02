namespace WebProducts.Models;

public record FullProductModel(int Code, string Name, decimal Price, int CategoryId, int CountryId, DateTime CreatedAt, DateTime EndDate); 

public record EditProductModel(int Code, string Name, decimal Price, int CountryId, DateTime CreatedAt, DateTime EndDate); 

public record CountryModel(string Name);

public record CategoryModel(string Name, int ParentId);

public record IdModel(int Id);

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string Error { get; private set; }
    public T Value { get; private set; }

    public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
    public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
}
