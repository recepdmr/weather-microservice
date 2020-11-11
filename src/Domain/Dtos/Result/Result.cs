namespace Domain.Dtos.Result
{
    public class Result : IResult
    {
        public Result(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}