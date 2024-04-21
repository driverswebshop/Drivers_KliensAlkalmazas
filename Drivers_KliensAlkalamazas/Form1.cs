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
using System.IO;
using Hotcakes.Modules.Core.Admin.Content;

namespace Drivers_KliensAlkalamazas
{
    public partial class Form1 : Form
    {
        static string url = "http://20.234.113.211:8085";
        static string key = "1-4a587ef4-be9b-4387-a1d7-e081245228a7";
        static Api proxy = new Api(url, key);
        static DataTable dataTable;
        static string[] prodId;
        static string[] invId;
        static string selectedBvin = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadProdInv();
            ListProd();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (selectedBvin == "")
            {
                leltarTextBox.Text = "0";
            }
            else
            {
                leltarTextBox.Text = GetInv(selectedBvin);
            }

        }

        private void ListProd()
        {
            var response = proxy.ProductsFindAll();

            JObject joResponse = JObject.Parse(response.ObjectToJson());
            JArray jArray = (JArray)joResponse["Content"];

            string[] keysToRemove = { "ProductTypeId", "CustomProperties", "ListPrice", "SitePriceOverrideText", "SiteCost", "MetaKeywords", "MetaDescription", "MetaTitle", "TaxExempt", "TaxSchedule", "ShippingDetails", "ShippingMode", "Status", "ImageFileSmall", "ImageFileSmallAlternateText", "ImageFileMediumAlternateText", "MinimumQty", "ShortDescription", "LongDescription", "ManufacturerId", "VendorId", "GiftWrapAllowed", "GiftWrapPrice", "Keywords", "PreContentColumnId", "PostContentColumnId", "Featured", "AllowReviews", "Tabs", "IsSearchable", "ShippingCharge" };
            foreach (JObject product in jArray)
            {
                foreach (var key in keysToRemove)
                {
                    product.Remove(key);
                }
            }

            //Console.WriteLine(jArray.ToString());

            dataTable = (DataTable)JsonConvert.DeserializeObject(jArray.ToString(), typeof(DataTable));
            dataGridView1.DataSource = dataTable;
        }

        private string GetInv(string bvin)
        {
            string getInvId;
            
            int index = Array.IndexOf(prodId, bvin.ToUpper());
            
            if (index == -1)
            {
                getInvId = "";
            }
            else
            {
                getInvId = invId[index];
            }

            var response = proxy.ProductInventoryFind(getInvId);

            JObject joResponse = JObject.Parse(response.ObjectToJson());
            JToken jObject = joResponse["Content"];;

            return jObject["QuantityOnHand"].ToString();
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            string name = NameTextBox.Text;
            string sku = SkuTextBox.Text;

            DataView dv = dataTable.DefaultView;
            dv.RowFilter = $"ProductName like '%{name}%' AND Sku like '%{sku}%'";
            DataTable filteredTable = dv.ToTable();

            dataGridView1.DataSource = filteredTable;
        }

        private void ReadProdInv()
        {
            string filePath = "prod_inv.csv";
            List<string> invIdL = new List<string>();
            List<string> prodIdL = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(';');

                    invIdL.Add(columns[0]);
                    prodIdL.Add(columns[1]);
                }

                prodId = prodIdL.ToArray();
                invId = invIdL.ToArray();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                selectedBvin = row.Cells["Bvin"].Value.ToString();
            }
        }
    }
}
