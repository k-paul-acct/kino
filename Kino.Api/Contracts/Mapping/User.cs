using Kino.ApiClient.Dto;
using Kino.Domain.Models;

namespace Kino.Api.Contracts.Mapping;

public static partial class Mapping
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            ImageUrl = user.ImageUrl,
            Username = user.UserName!,
            Role = user.Role!.MapToDto(),
        };
    }
}