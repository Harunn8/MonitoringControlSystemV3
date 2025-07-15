using McsMqtt.Connection.Base;
using MQTTnet.Client;
using System;
using Serilog;
using MQTTnet.Client.Options;
using System.Threading;

namespace McsMqtt.Connection
{
    public class MqttConnection : IMqttConnection
    {
        IMqttClientOptions _options;
        private IMqttClient _mqttClient;
        private bool _disposed;

        public MqttConnection(IMqttClientOptions options, IMqttClient mqttClient)
        {
            _options = options;
            _mqttClient = mqttClient;

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
                _mqttClient.UseDisconnectedHandler(e =>
                {
                    Log.Information("Disconnected from MQTT Broker. Reconnecting in 5 seconds...");
                    var brokerCount = 0;
                    while(brokerCount == 5)
                    {
                        Thread.Sleep(500);
                        _mqttClient.ReconnectAsync();
                        if(_mqttClient.IsConnected) 
                        {
                            Log.Information("Connected to MQTT Broker.");
                            break;
                        }
                        brokerCount++;
                        Log.Warning("Could not connect to MQTT Broker.");
                    }
                });

            }
            catch (Exception ex)
            {
                Log.Error("Failed to established MQTT connection: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
