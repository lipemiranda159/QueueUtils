namespace QueueUtility.Models
{
    public class StatusMessageProduced
    {
        public bool Success { get; set; }
        public string Reason { get; set; }

        public StatusMessageProduced()
        {
            Success = false;
        }
    }
}
