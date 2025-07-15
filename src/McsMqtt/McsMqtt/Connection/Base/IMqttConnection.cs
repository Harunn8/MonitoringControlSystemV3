using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsMqtt.Connection.Base
{
    public interface IMqttConnection : IDisposable
    {
        void TryConnect();
        IMqttClient GetMqttClient();
    }
}
