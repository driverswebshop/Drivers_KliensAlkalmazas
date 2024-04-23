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
using Drivers_KliensAlkalamazas.Abstractions;
using Drivers_KliensAlkalamazas.Services;
using Drivers_KliensAlkalamazas.Controllers;

namespace Drivers_KliensAlkalamazas
{
    public partial class Form1 : Form
    {
        private SzuresController _controller = new SzuresController();
        private LeltarController _lcontroller = new LeltarController();


        static string url = "http://20.234.113.211:8085";
        static string key = "1-4a587ef4-be9b-4387-a1d7-e081245228a7";
        static Api proxy = new Api(url, key);
        static DataTable dataTable;
        static string[] prodId;
        static string[] invId;
        static string getInvId;
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
            Console.WriteLine("macilaci");
            var inventory = proxy.ProductInventoryFind(getInvId).Content;
            Console.WriteLine(inventory);
            try
            {
                int valos=_lcontroller.Validalas(ValosTextBox.Text);
                inventory.QuantityOnHand = valos;
                ApiResponse<ProductInventoryDTO> response = proxy.ProductInventoryUpdate(inventory);

                Console.WriteLine(response);

                leltarTextBox.Text = GetInv(selectedBvin);

                MessageBox.Show("Leltáradatok sikeresen frissítve!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ValosTextBox.Text = "";
        }

        private void ListProd()
        {
            var response = proxy.ProductsFindAll();

            JArray jArray = new JArray();
            try
            {
                JObject joResponse = JObject.Parse(response.ObjectToJson());
                jArray = (JArray)joResponse["Content"];
            }
            catch (Exception err)
            {
                Console.WriteLine("Hiba a termékek lekérdezése közben: " + err);
            }
            

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
            int index = Array.IndexOf(prodId, bvin.ToUpper());
            
            if (index == -1)
            {
                getInvId = "";
            }
            else
            {
                getInvId = invId[index];
            }

            var response = proxy.ProductInventoryFind(getInvId).Content;

            return response.QuantityOnHand.ToString();
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                string sku = SkuTextBox.Text;

                _controller.Validalas(sku);

                DataView dv = dataTable.DefaultView;
                dv.RowFilter = $"ProductName like '%{name}%' AND Sku like '%{sku}%'";
                DataTable filteredTable = dv.ToTable();

                dataGridView1.DataSource = filteredTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

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

                leltarTextBox.Text = GetInv(selectedBvin);
            }
        }
    }
}
