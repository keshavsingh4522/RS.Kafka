using Confluent.Kafka;

namespace RS.Service.Core;

public class KafkaProducerService(string bootstrapServers, string topicName)
{
    private readonly string _bootstrapServers = bootstrapServers;
    private readonly string _topicName = topicName;

    public async Task SendMessageAsync(string message)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            try
            {
                var result = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message });
                Console.WriteLine($"Message sent to {_topicName} partition [{result.Partition.Value}] at offset {result.Offset.Value}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Message} [{e.Error.Code}]");
                throw;
            }
        }
    }
}
