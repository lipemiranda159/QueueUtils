using Confluent.Kafka;
using QueueUtility.Interfaces;
using QueueUtility.Models;
using System;
using System.Threading.Tasks;

namespace QueueUtility
{
    public class KafkaProducer<T> : QueueUtilityBase, IQueueProducer<T, StatusMessageProduced> where T : class
    {
        private readonly string _kafkaConnection;
        protected KafkaProducer(string queueName, string kafkaConnection) : base(queueName)
        {
            _kafkaConnection = kafkaConnection;
        }

        public async Task<StatusMessageProduced> SendDataAsync(T message)
        {
            var result = new StatusMessageProduced();
            var bootstrapServers = _kafkaConnection;
            var nomeTopic = QueueName;


            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };
            try
            {
                using var producer = new ProducerBuilder<Null, T>(config).Build();
                await producer.ProduceAsync(
                    nomeTopic,
                    new Message<Null, T>
                    { Value = message });
            }
            catch (Exception ex)
            {
                result.Reason = ex.Message;
            }

            return result;

        }
    }

}

