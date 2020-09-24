using Confluent.Kafka;
using QueueUtility.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueUtility
{
    public class KafkaConsumer : QueueUtilityBase, IQueueConsumer
    {
        private bool IsRunning;
        private IMessageReceiver MessageReceiver;
        private readonly ConsumerConfig _consumerConfig;
        private readonly string _kafkaConnection;

        protected KafkaConsumer(string queueName, string kafkaConnection) : base(queueName)
        {
            _kafkaConnection = kafkaConnection;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaConnection,
                GroupId = $"{QueueName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async Task StartConsumerAsync(IMessageReceiver messageReceiver, CancellationTokenSource cancellationToken)
        {
            MessageReceiver = messageReceiver;

            Task
                .Factory
                .StartNew(() => ConsumeAsync(cancellationToken.Token),
                            TaskCreationOptions.LongRunning)
                .Unwrap();

        }

        private async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            consumer.Subscribe(QueueName);

            try
            {
                while (IsRunning)
                {
                    var cr = consumer.Consume(cancellationToken);
                    await MessageReceiver.ProcessMessageAsync(cr.Message.Value);
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task StopConsumerAsync()
        {
            IsRunning = false;
        }
    }
}
