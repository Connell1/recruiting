using MediatR;

namespace Flipdish.Recruiting.Domain.Bus
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse> where TResponse : ICommandResponse
    {

    }
}
