using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;
using System.Net;

namespace Shop.API.Models.Result;

public abstract class AResult<TDbo, TDto, TResult> : AResult where TDbo : BaseDbo
    where TDto : BaseDto<TDbo, TDto>
    where TResult : AResult<TDbo, TDto, TResult>, new()
{
    public List<TDto> DtoList { get; private init; }

    public static TResult FromDto(HttpStatusCode statusCode, TDto dto) => new TResult { DtoList = new List<TDto> { dto }, ResponseBody = dto, StatusCode = statusCode };

    public static TResult FromDtoList(HttpStatusCode statusCode, List<TDto> dtoList) => new TResult { DtoList = dtoList, ResponseBody = dtoList, StatusCode = statusCode };

    public static new TResult FromMessage(HttpStatusCode statusCode, string message) => new TResult { ResponseBody = message, StatusCode = statusCode };
    public static new TResult FromCount(HttpStatusCode statusCode, int count) => new TResult { ResponseBody = count, StatusCode = statusCode };
}

public abstract class AResult<TData, TResult> : AResult where TResult : AResult<TData, TResult>, new()
{
    public TData Data { get => (TData)ResponseBody; }

    public static TResult FromData(HttpStatusCode statusCode, TData data) => new TResult { ResponseBody = data, StatusCode = statusCode };

    public static new TResult FromMessage(HttpStatusCode statusCode, string message) => new TResult { ResponseBody = message, StatusCode = statusCode };
}

public class AResult
{
    protected AResult()
    {
    }

    public HttpStatusCode StatusCode { get; protected set; }
    public object ResponseBody { get; protected set; }

    public static AResult FromMessage(HttpStatusCode statusCode, string message) => new AResult { ResponseBody = message, StatusCode = statusCode };
}


