using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Dtos.Comments;
using dotnet8_api.Dtos.Stock;
using dotnet8_api.Models;

namespace dotnet8_api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto 
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content
            };
        }
    }
}