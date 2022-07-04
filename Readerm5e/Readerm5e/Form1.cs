using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThingMagic;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Management;
using System.Text.RegularExpressions;
using Cursors = System.Windows.Input.Cursors;

namespace Readerm5e
{
    public partial class Form1 : Form
    {
        Reader objReader = null;

        public Form1()
        {
            InitializeComponent();
            InitializeReaderUriBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Click Made");
            
            
            try
            {
                if (objReader != null)
                {
                    System.Diagnostics.Debug.WriteLine("Entra al IF");

                    objReader.Destroy();
                    //ConfigureAntennaBoxes(null);
                    //ConfigureLogicalAntennaBoxes(null);
                    //ConfigureProtocols(null);
                }

                string readerUri = cmbReaderPort.Text;

                MatchCollection mc = Regex.Matches(readerUri, @"(?<=\().+?(?=\))");
                foreach (Match m in mc)
                {
                    if (!string.IsNullOrWhiteSpace(m.ToString()))
                        readerUri = m.ToString();
                }

                objReader = Reader.Create(string.Concat("tmr:///", readerUri));
                //objReader = Reader.Create("eapi:///com5");
                objReader.Connect();
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(readerUri));
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(objReader));

                //objReader.StartReading();

                
            }
            catch (Exception)
            {

                throw;
            }
        }



        private void InitializeReaderUriBox()
        {
            try
            {
                Mouse.SetCursor(Cursors.Wait);
                
                List<string> portNames = GetComPortNames();

                foreach (string portName in portNames)
                {
                    cmbReaderPort.Items.Add(portName);
                }

                
                if (portNames.Count > 0)
                {
                    cmbReaderPort.Text = portNames[0];
                }
                
                Mouse.SetCursor(Cursors.Arrow);
            }
            catch (Exception bonjEX)
            {
                Mouse.SetCursor(Cursors.Arrow);
                throw bonjEX;
            }
        }

        private List<string> GetComPortNames()
        {
            List<string> portNames = new List<string>();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0"))
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if ((queryObj != null) && (queryObj["Name"] != null))
                    {
                        if (queryObj["Name"].ToString().Contains("(COM"))
                            portNames.Add(queryObj["Name"].ToString());
                    }
                }
            }
            return portNames;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                TagReadData[] tagList = objReader.Read(100);

                foreach (TagReadData tag in tagList)
                {
                    System.Diagnostics.Debug.WriteLine("EPC: " + tag.EpcString);
                    System.Diagnostics.Debug.WriteLine("prd: " + JsonConvert.SerializeObject(tag.prd));
                    System.Diagnostics.Debug.WriteLine("Data: " + JsonConvert.SerializeObject(tag.Data));
                    System.Diagnostics.Debug.WriteLine("Tag: " + tag.Tag);
                    System.Diagnostics.Debug.WriteLine("Tag: " + tag.ReadCount);
                    lblEPC.Text = tag.EpcString;
                }
                System.Diagnostics.Debug.WriteLine(tagList);
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(tagList));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
