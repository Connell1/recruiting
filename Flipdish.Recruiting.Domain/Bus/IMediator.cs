using System.Threading.Tasks;

namespace Flipdish.Recruiting.Domain.Bus
{
    public interface IMediator
    {
        Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse> where TResponse : ICommandResponse;
    }
}
