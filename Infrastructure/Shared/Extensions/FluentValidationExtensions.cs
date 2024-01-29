using FluentValidation.Results;
using Kedy.Result;

namespace Shared.Extensions;

public static class FluentValidationExtensions
{
    public static T ToResult<T>(this ValidationResult validationResult)
        where T : IResult
    {
        var typeFullName = typeof(T).FullName;
        var typeName = typeof(T).Name;
        
        
        // var result = new T();
        // if (validationResult.IsValid)
        // {
        //     result.HasError = false;
        //     return result;
        // }
        //
        // result.HasError = true;
        // result.Errors = validationResult.Errors
        //     .Select(x => new KeyValuePair<string, string>(x.PropertyName, $"{x.ErrorMessage}")).ToList();
        // result.Message = "Validasyon hatası";
        // return result;
        return default;
    }

    public static T ToResult<T>(this ValidationResult[] validationResults)
        where T : IResult
    {
        var typeName = typeof(T).FullName;
        
        
        
        // if (validationResults.All(x => x.IsValid))
        // {
        //     result.HasError = false;
        //     return result;
        // }
        //
        // result.HasError = true;
        // foreach (var validationResult in validationResults)
        // {
        //     result.Errors = validationResult.Errors
        //         .Select(x => new KeyValuePair<string, string>(x.PropertyName, $"{x.ErrorMessage}")).ToList();
        // }
        //
        // result.Message = "Validasyon hatası";
        // return result;
        return default;
    }
}