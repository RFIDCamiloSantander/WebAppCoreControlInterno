using Impinj.OctaneSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Impinj
{
    public class Reader
    {
        //Lista para los reportes
        public static List<string> epcList = new List<string>();


        const string READER_HOSTNAME = "SpeedwayR-10-29-88";  // NEED to set to your speedway!
        // Create an instance of the ImpinjReader class.
        static ImpinjReader reader = new ImpinjReader();

        public async Task<List<string>> CrearLectura()
        {
            try
            {

                epcList.Clear();

                //Se asigna un nombre al reader.
                reader.Name = "TestName";

                //Se conecta al reader
                ConnectToReader();

                // Get the default settings.
                // We'll use these as a starting point
                // and then modify the settings we're 
                // interested in.
                Settings settings = reader.QueryDefaultSettings();

                // Start the reader as soon as it's configured.
                // This will allow it to run without a client connected.
                //settings.AutoStart.PeriodInMs = 100;
                //settings.AutoStart.Mode = AutoStartMode.Immediate;


                //Aqui tengo que hacer algo

                //settings.AutoStop.DurationInMs = 100;
                //settings.AutoStop.Mode = AutoStopMode.Duration;


                // Use Advanced GPO to set GPO #1 
                // when an client (LLRP) connection is present.
                settings.Gpos.GetGpo(1).Mode = GpoMode.LLRPConnectionStatus;



                // Tell the reader to include the timestamp in all tag reports.
                settings.Report.IncludeFirstSeenTime = true;
                settings.Report.IncludeLastSeenTime = true;
                settings.Report.IncludeSeenCount = true;


                // Enable keepalives.
                //settings.Keepalives.Enabled = true;
                //settings.Keepalives.PeriodInMs = 5000;


                // Enable link monitor mode.
                // If our application fails to reply to
                // five consecutive keepalive messages,
                // the reader will close the network connection.
                //settings.Keepalives.EnableLinkMonitorMode = true;
                //settings.Keepalives.LinkDownThreshold = 5;


                // Apply the newly modified settings.
                reader.ApplySettings(settings);



                // Assign an event handler that will be called
                // when keepalive messages are received.
                reader.KeepaliveReceived += OnKeepaliveReceived;


                // Assign an event handler that will be called
                // if the reader stops sending keepalives.
                reader.ConnectionLost += OnConnectionLost;


                // Assign the TagsReported event handler.
                // This specifies which method to call
                // when tags reports are available.
                reader.TagsReported += OnTagsReported;


                reader.Start();

                await Task.Delay(100);

                reader.Stop();

                return epcList;

                //return Re
                //System.Diagnostics.Debug.WriteLine( JsonConvert.SerializeObject(settings) );
                //return settings; Este entrega un JSON con los settings.
                //return RedirectToAction(nameof(Index));
            }
            catch (OctaneSdkException e)
            {
                // Handle Octane SDK errors.
                System.Diagnostics.Debug.WriteLine("Octane SDK exception: {0}", e.Message);
                Console.WriteLine("Octane SDK exception: {0}", e.Message);
                throw e;
            }
            catch (Exception e)
            {
                // Handle other .NET errors.
                System.Diagnostics.Debug.WriteLine("Exception : {0}", e.Message);
                Console.WriteLine("Exception : {0}", e.Message);
                throw;
            }
        }


        //Coneccion al lector
        static void ConnectToReader()
        {
            try
            {
                Console.WriteLine("Attempting to connect to {0} ({1}).",
                    reader.Name, READER_HOSTNAME);

                System.Diagnostics.Debug.WriteLine("Attempting to connect to {0} ({1}).",
                    reader.Name, READER_HOSTNAME);

                // The maximum number of connection attempts
                // before throwing an exception.
                //reader.MaxConnectionAttempts = 15;

                // Number of milliseconds before a 
                // connection attempt times out.
                reader.ConnectTimeout = 6000;

                // Connect to the reader.
                // Change the ReaderHostname constant in SolutionConstants.cs 
                // to the IP address or hostname of your reader.
                reader.Connect(READER_HOSTNAME);
                Console.WriteLine("Successfully connected.");
                System.Diagnostics.Debug.WriteLine("Successfully connected.");

                // Tell the reader to send us any tag reports and 
                // events we missed while we were disconnected.
                reader.ResumeEventsAndReports();
            }
            catch (OctaneSdkException e)
            {
                Console.WriteLine("Failed to connect.");
                System.Diagnostics.Debug.WriteLine("Failed to connect.");
                throw e;
            }
        }

        static void OnConnectionLost(ImpinjReader reader)
        {
            // This event handler is called if the reader  
            // stops sending keepalive messages (connection lost).
            System.Diagnostics.Debug.WriteLine("Connection lost : {0} ({1})", reader.Name, reader.Address);
            Console.WriteLine("Connection lost : {0} ({1})", reader.Name, reader.Address);

            // Cleanup
            reader.Disconnect();

            // Try reconnecting to the reader
            ConnectToReader();
        }

        static void OnKeepaliveReceived(ImpinjReader reader)
        {
            // This event handler is called when a keepalive 
            // message is received from the reader.
            System.Diagnostics.Debug.WriteLine("Keepalive received from {0} ({1})", reader.Name, reader.Address);
            Console.WriteLine("Keepalive received from {0} ({1})", reader.Name, reader.Address);
        }

        static void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            // This event handler is called asynchronously 
            // when tag reports are available.
            // Loop through each tag in the report 
            // and print the data.
            foreach (Tag tag in report)
            {
                if (!epcList.Exists(r => r == tag.Epc.ToHexString()))
                {
                    epcList.Add(tag.Epc.ToHexString());
                    System.Diagnostics.Debug.WriteLine("EPC : {0} Timestamp : {1}", tag.Epc.ToHexString(), tag.LastSeenTime);
                    Console.WriteLine("EPC : {0} Timestamp : {1}", tag.Epc, tag.LastSeenTime);
                    System.Diagnostics.Debug.WriteLine("EPC normal: {0} - EPC Hex: {1}", tag.Epc, tag.Epc.ToHexString());
                    //System.Diagnostics.Debug.WriteLine( JsonConvert.SerializeObject(tag) );
                    
                }
            }
        }
    }
}
