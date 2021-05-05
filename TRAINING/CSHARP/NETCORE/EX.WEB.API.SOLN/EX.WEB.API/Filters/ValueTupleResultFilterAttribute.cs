using EX.DTO.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX.WEB.API.Filters
{
    public class ValueTupleResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next
        )
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode > 300)
            {
                await next();
                return;
            }
            // Always use this syntax when using tuples
            var (authorId, authors) = ((int? id, IEnumerable<AuthorDto> authors))resultFromAction.Value;
            // Use AutoMapper to map the above object into one
            //resultFromAction.Value = new
            //{
            //    id: authorId,
            //    authorResponse: authors
            //};
            resultFromAction.Value = new AuthorExtension
            {
                authorIdentifier = authorId,
                authorResponses = authors
            };
            await next();
        }
    }

    internal class AuthorExtension
    {
        public int? authorIdentifier { get; set; }
        public IEnumerable<AuthorDto> authorResponses { get; set; }
    }
}
