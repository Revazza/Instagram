using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using MapsterMapper;
using MediatR;

namespace Instagram.Application.DomainEntities.Users.Queries.FilterUsersByUserName;

public class FilterUsersByUserNameQueryHandler : IRequestHandler<FilterUsersByUserNameQuery, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public FilterUsersByUserNameQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(FilterUsersByUserNameQuery request, CancellationToken cancellationToken)
    {
        var filteredUsers = await _userRepository.FilterUsersByUserNameAsync(request.UserName);
        var mappedFilteredUsers = _mapper.Map<List<User>, List<FilterUsersByUserNameResponse>>(filteredUsers);

        return Response.Ok().Add("filteredUsers", mappedFilteredUsers);
    }
}