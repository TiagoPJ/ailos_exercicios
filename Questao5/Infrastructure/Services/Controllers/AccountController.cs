using IdempotentAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Idempotent(Enabled = true)]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("GetAccount")]
        [SwaggerOperation(Summary = "Obtém a conta do usuário.")]
        public async Task<IResult> GetAccountById(
            [FromServices] IMediator mediator,
            [FromQuery] FindAccountByIdRequest command
        )
        {
            var response = await mediator.Send(command);
            return response.Match(
              Results.Ok,
              Results.BadRequest
            );
        }

        [HttpPost]
        [Route("AddMoviment")]
        [SwaggerOperation(Summary = "Adiciona movimentação na conta do usuário.")]
        public async Task<IResult> AddMoviment(
                   [FromServices] IMediator mediator,
                   [FromBody] CreateMovimentRequest command
               )
        {
            var response = await mediator.Send(command);
            return response.Match(
              Results.Ok,
              Results.BadRequest
            );
        }
    }
}
