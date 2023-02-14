namespace API.Errors;

public class ErrorResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public List<DetailsResponse> details { get; set; }
}
