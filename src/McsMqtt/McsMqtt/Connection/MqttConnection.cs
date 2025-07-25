using McsMqtt.Connection.Base;
using MQTTnet.Client;
using System;
using Serilog;
using MQTTnet.Client.Options;
using System.Threading;
using McsMqtt.Settings;
using Microsoft.Extensions.Options;

namespace McsMqtt.Connection
{
    public class MqttConnection : IMqttConnection
    {
        IMqttClientOptions _options;
        private IMqttClient _mqttClient;
        private bool _disposed;
        private readonly MqttSettings _settings;

        public MqttConnection(IMqttClientOptions options, IMqttClient mqttClient, IOptions<MqttSettings> settings)
        {
            _options = options;
            _mqttClient = mqttClient;
            _settings = settings.Value;

            if (!_mqttClient.IsConnected)
            {
                TryConnect();
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            try
            {
                _mqttClient.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IMqttClient GetMqttClient()
        {
            if(!_mqttClient.IsConnected && !_disposed)
            {
                TryConnect();
            }
            return _mqttClient;
        }

        public void TryConnect()
        {
                try
                {
                    var options = new MqttClientOptionsBuilder()
                        .WithClientId("telemetry")
                        .WithTcpServer(_settings.IpAddress, _settings.Port)
                        .Build();

                    _mqttClient.ConnectAsync(options, CancellationToken.None).Wait();

                    Log.Information("Connected to MQTT Broker");
                }
                catch (Exception ex)
                {
                    Log.Error("Failed to connect: {Message}", ex.Message);
                }
            
            }
    }
}
