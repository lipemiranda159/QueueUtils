namespace QueueUtility
{
    public abstract class QueueUtilityBase
    {
        private readonly string _queueName;
        protected QueueUtilityBase(string queueName)
        {
            _queueName = queueName;
        }

        public string QueueName => _queueName;
    }
}
