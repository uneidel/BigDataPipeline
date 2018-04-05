using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassicSearchHelper
{
    public partial class Form1 : Form
    {


        AzureSearchHelper helper = null;
        public Form1()
        {
            helper = new AzureSearchHelper();
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            helper.CreateClient(Log);
        }

       

       
        private void Log(string message)
        {
            StatusBox.Text += message + Environment.NewLine;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var status = helper.GetStatus();
                Log($"Status: { status.ToString()}");
            }
            catch
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = helper.SimpleSearch(this.textBox1.Text);
            Log($"Count: {result.Results.Count}"  +  Environment.NewLine);
            /*result.Results.ToList().ForEach(x =>
            {
                this.textBox2.Text += $"SCore: {x.Score} \r\n" ;
            });
            */
            
        }
    }
}
