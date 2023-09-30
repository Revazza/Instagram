using Instagram.Application.Common.Responses;
using Instagram.Domain.Stories;
using Mapster;

namespace Instagram.Application.Mappings;

public class StoryMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Story, GenericStoryResponse>()
            .Map(dest => dest.Id, src => src.StoryId.Value)
            .Map(dest => dest.Author, src => src.Author.Adapt<GenericUserResponse>())
            .Map(dest => dest.Url, src => Convert.ToBase64String(src.MediaData))
            .Map(dest => dest.Category, src => src.MediaType.Split('/', StringSplitOptions.TrimEntries).First())
            .Map(dest => dest.Format, src => src.MediaType.Split('/', StringSplitOptions.TrimEntries).Last());

    }
}