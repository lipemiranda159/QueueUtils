using System.Threading;
using System.Threading.Tasks;

namespace QueueUtility.Interfaces
{
    public interface IQueueConsumer 
    {
        Task StartConsumerAsync(IMessageReceiver messageReceiver, CancellationTokenSource cancellationToken);
        Task StopConsumerAsync();
    }
}
