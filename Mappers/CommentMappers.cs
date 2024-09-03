using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Comment;

namespace Api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto commentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,

            };
        }

        public static Comment CommentCreated(this CreateCommentRequestDto createCommentRequestDto)
        {
            return new Comment
            {
                Title = createCommentRequestDto.Title,
                Content = createCommentRequestDto.Content,
                StockId = createCommentRequestDto.StockId,
            };
        }
    }

}