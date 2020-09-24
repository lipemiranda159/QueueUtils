using System.Threading.Tasks;

namespace QueueUtility.Interfaces
{
    public interface IMessageReceiver
    {
        Task ProcessMessageAsync(string message);
    }
}
