using System;
using McsMqtt.Connection.Base;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace McsMqtt.Producer
{
    public class MqttProducer
    {
        private readonly IMqttConnection _connection;

        public MqttProducer(IMqttConnection connection)
        {
            _connection = connection;
        }

        public virtual bool GetMqttConnectionStatus()
        {
            return _connection.GetMqttClient().IsConnected;
        }

        public virtual void PublishMessage(string topic, string message, MqttQualityOfServiceLevel qosLevel = MqttQualityOfServiceLevel.AtMostOnce)
        {
            var client = _connection.GetMqttClient();
            {
                var sendingMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(qosLevel)
                    .Build();

                client.PublishAsync(sendingMessage).GetAwaiter();
            }
        }
    }
}
