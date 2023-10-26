﻿using Shared.Auth;
using Shared.Dtos;

namespace HttpClients.ClientInterface;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
}