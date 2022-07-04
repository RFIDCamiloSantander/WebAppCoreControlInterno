using ThingMagic;
using System.IO;

namespace m5eReader
{
    public partial class Form1 : Form
    {
        Reader Reader = null;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "holiwi";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Click Made");
            System.Diagnostics.Debug.WriteLine(textBox1.Text);
            Reader = Reader.Create(string.Concat("tmr:///", textBox1.Text));
        }
    }
}