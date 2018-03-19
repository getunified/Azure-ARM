using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "{IoT Hub URI}";
        static string deviceKey = "{Device Key}";

        Dictionary<int, string> sensorTypes = new Dictionary<int, string>()
        {
            { 1, "temp" },
            { 2, "amps" },
            { 3, "amps" },
            { 4, "temp" },

        };

        private static async void SendDeviceToCloudMessagesAsync()
        {
            int count = 0;
            Program newProg = new Program();
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"{Insert Address of Directory Here}");
            //string line = file.ReadLine();
            //while ((line != null) || (line != "") || (line.Length != 0))
            while ((line = file.ReadLine()) != null)
            {
                string macAddress = line.Substring(26, 17);

                // Acquire JSON after 65 characters
                line = line.Substring(65);


                line = newProg.ModifyMessage(line, macAddress);

                string unixtime = line.Substring(61, 13);
                Console.WriteLine(line);
                Console.WriteLine(unixtime);
                double dunixtime = (Convert.ToDouble(unixtime)) / 1000.0;

                System.DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                datetime = datetime.AddSeconds(dunixtime).ToLocalTime();

                string sdatetime = datetime.ToString("yyyy-MM-ddThh:mm:ss", CultureInfo.InvariantCulture);

                line = line.Replace(unixtime, '"' + sdatetime + '"');

                var message = new Message(Encoding.ASCII.GetBytes(line));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, line);

                await Task.Delay(1000);
                count++;
            }

      
            file.Close();
            System.Console.WriteLine("There were {0} lines.", count);
            // Suspend the screen.  
            System.Console.ReadLine();
        }

        private string ModifyMessage(string line, string MACAddress)
        {

                string[] lineSplit = line.Split(','); //{"id":3
                string[] lineSplitSplit = lineSplit[0].Split(':'); //['{"id"', '3'] 
                Int32.TryParse(lineSplitSplit[1], out int sensorTypeID);

                string sensorTypeString = sensorTypes[sensorTypeID];

                line = line.Replace("cts", sensorTypeString + sensorTypeID + "cts");
                line = line.Replace("cv", sensorTypeString + sensorTypeID + "cv");

                line = line.Substring(0, 7) + ",\"mac\":\"" + MACAddress + "\"" + line.Substring(7);

                Console.WriteLine("************************************");
                Console.WriteLine("Line: " + line);

                if (line != null)
                {
                    return line;
                }
                else {
                
                    Console.WriteLine("Line is NULL");
                    return line;
                }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("TestDevice", deviceKey), TransportType.Mqtt);
            deviceClient.ProductInfo = "HappyPath_Simulated-CSharp";
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

    
    }
}
