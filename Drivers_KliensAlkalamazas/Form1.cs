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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            var response = proxy.ProductsFind("7C6F5649-23C5-4D2E-BB6D-EDC969AD4B92");

            JObject joResponse = JObject.Parse(response.ObjectToJson());
            JObject jObject = (JObject)joResponse["Content"];
            //jObject.Remove("Bvin");
            string bvin = jObject["Bvin"].ToString();

            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(bvin, typeof(DataTable));
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bvin = "b6dd2a96-81e3-4da7-998b-8eed2ba03759";

            var response = proxy.ProductInventoryFind(bvin);
            //var response = proxy.ProductsFind(bvin);
            Console.WriteLine(response.ObjectToJson());

        }
    }
}
