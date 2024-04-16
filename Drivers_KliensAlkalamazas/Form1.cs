using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using Hotcakes.CommerceDTO.v1.Client;
using System.Runtime.Remoting.Proxies;
using Hotcakes.CommerceDTO.v1.Catalog;
using Hotcakes.CommerceDTO.v1;
using Hotcakes.Web;

namespace Drivers_KliensAlkalamazas
{
    public partial class Form1 : Form
    {
        static string url = "http://20.234.113.211:8085";
        static string key = "1-4a587ef4-be9b-4387-a1d7-e081245228a7";
        static Api proxy = new Api(url, key);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bvin = "7c6f5649-23c5-4d2e-bb6d-edc969ad4b92";

            var response = proxy.ProductInventoryFind(bvin);
            //var response = proxy.ProductsFind(bvin);
            Console.WriteLine(response.ObjectToJson());

        }
    }
}
