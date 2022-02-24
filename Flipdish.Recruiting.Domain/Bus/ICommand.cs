using MediatR;

namespace Flipdish.Recruiting.Domain.Bus
{
    public interface ICommand<T> : IRequest<T> where T : ICommandResponse
    {

    }
}
