
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

//MQTT
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
namespace GetUnifiedClient
{
    class Device
    {
        //DeviceClient deviceClient;

        //string iotHubUri = "msagetunifiedhub.azure-devices.net";
        //string deviceKey = "3YIFt8NgcyyWheikGcbIoPEIc45codr/pb8gvHFpvoc=";
        TextBlock tb1, tb2, tb3, tb4, tb5;
        int counter = 0;
        public Device(TextBlock TextBlock1, TextBlock TextBlock2, TextBlock TextBlock3, TextBlock TextBlock4, TextBlock TextBlock5)
        {
            //deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("TestDevice", deviceKey), TransportType.Mqtt);
            //deviceClient.ProductInfo = "HappyPath_Simulated-CSharp";
            tb1 = TextBlock1;
            tb2 = TextBlock2;
            tb3 = TextBlock3;
            tb4 = TextBlock4;
            tb5 = TextBlock5;

            tb1.Text = "Waiting";
            tb2.Text = "Waiting";
            tb3.Text = "Waiting";
            tb4.Text = "Waiting";
            InitializeMQTT();
        }

        private void InitializeMQTT()
        {
            string hostAddress = "ENTER ADDRESS HERE";
            string username = "ENTER USERNAME HERE";
            string password = "ENTER PASSWORD HERE";
            MqttClient myClient = new MqttClient(hostAddress);

            myClient.MqttMsgPublishReceived += Client_PublishReceived;
            string clientId = Guid.NewGuid().ToString();
            myClient.Connect(clientId, username, password);
            myClient.Subscribe(new string[] { "#" }, new byte[] { 1 });

            if (myClient.IsConnected)
                tb5.Text = "CONNECTED BRUH";
        }

        private void Client_PublishReceived(object Sender, MqttMsgPublishEventArgs e)
        {
            //tb1.Text = "YO WAT";
            string message = System.Text.Encoding.UTF8.GetString(e.Message);
            ParseMessage(message, e.Topic);
            
            //System.Console.WriteLine(message);
        }

        private async void ParseMessage(string message, string topic)
        {
            JMessage converted = JsonConvert.DeserializeObject<JMessage>(message);
            string[] topicArray = topic.Split('/');
            string macAddress = topicArray[3];
            string tempId = converted.id;
            string id = topicArray[topicArray.Length - 1];
            //Debug.WriteLine("id is: " + id);
            converted.id = macAddress + "-" + id;//tempId;
            //Console.WriteLine(converted.id);

            string unixtime = converted.data.cts;
            double dunixtime = (Convert.ToDouble(unixtime)) / 1000.0;
            System.DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            datetime = datetime.AddSeconds(dunixtime).ToLocalTime();
            converted.data.cts = datetime.ToString("yyyy-MM-ddThh:mm:ss", CultureInfo.InvariantCulture);

            string output = JsonConvert.SerializeObject(converted);

            if (converted.data.cvu.Contains("&deg;C"))
            {
                //Console.WriteLine("DEGREES");
                output = output.Replace("cv", "temp" + id + "cv");
            }
            else if (converted.data.cvu.Contains(" A"))
            {
                //Console.WriteLine("AMP");
                output = output.Replace("cv", "amp" + id + "cv");
            }

            Debug.WriteLine(output);
            //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, output);
            
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                //tb5.Text = id;
                switch (id)
                {
                   case "1":
                       tb1.Text = output;
                       break;
                   case "2":
                       tb2.Text = output;
                       break;
                   case "3":
                       tb3.Text = output;
                       break;
                   case "4":
                       tb4.Text = output;
                       break;
                }
                tb5.Text = "Completed" + counter++;
                return;
            });
            
            //var iotmessage = new Message(Encoding.ASCII.GetBytes(output));

            //await deviceClient.SendEventAsync(iotmessage);
        }
    }
}

