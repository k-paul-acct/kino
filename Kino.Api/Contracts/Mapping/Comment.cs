using Kino.ApiClient.Dto;
using Kino.Domain.Models;

namespace Kino.Api.Contracts.Mapping;

public static partial class Mapping
{
    public static CommentDto MapToDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            TitleId = comment.TitleId!.Value,
            UserId = comment.UserId!.Value,
            Username = comment.User!.UserName!,
            Text = comment.TextContent!,
            Date = comment.Date,
        };
    }
}