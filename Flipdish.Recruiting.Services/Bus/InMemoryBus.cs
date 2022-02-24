using Flipdish.Recruiting.Domain.Bus;
using System.Threading.Tasks;

namespace Flipdish.Recruiting.Services.Services
{
    public sealed class InMemoryBus : IMediator
    {
        private readonly MediatR.IMediator _mediator;

        public InMemoryBus(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command)
            where TCommand : ICommand<TResponse>
            where TResponse : ICommandResponse
        {
            return await _mediator.Send(command);
        }
    }
}
