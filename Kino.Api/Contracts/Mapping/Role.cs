using Kino.ApiClient.Dto;
using Kino.Domain.Models;

namespace Kino.Api.Contracts.Mapping;

public static partial class Mapping
{
    public static RoleDto MapToDto(this Role role)
    {
        return new RoleDto { Id = role.Id, RoleName = role.RoleName!, };
    }
}