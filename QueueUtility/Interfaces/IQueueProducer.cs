using System.Threading.Tasks;

namespace QueueUtility.Interfaces
{
    public interface IQueueProducer<T, StatusMessageProduced> where T : class
    {
        Task<StatusMessageProduced> SendDataAsync(T message);
    }
}
