namespace Instagram.Application.Common.Responses;

public class GenericUserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;


    public GenericUserResponse()
    {
        
    }

}

