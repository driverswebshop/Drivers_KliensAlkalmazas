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

namespace Drivers_KliensAlkalamazas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Console.WriteLine("This is an API Sample Program for Hotcakes");
            Console.WriteLine();

            var url = "http://http://20.234.113.211:8085//DesktopModules/Hotcakes/API/rest/v1/";
            var key = "1-4a587ef4-be9b-4387-a1d7-e081245228a7";

            var proxy = new Api(url, key);

            var snaps = proxy.CategoriesFindAll();
            if (snaps.Content != null)
            {
                Console.WriteLine("Found " + snaps.Content.Count + " categories");
                Console.WriteLine("-- First 5 --");
                for (var i = 0; i < 5; i++)
                {
                    if (i < snaps.Content.Count)
                    {
                        Console.WriteLine(i + ") " + snaps.Content[i].Name + " [" + snaps.Content[i].Bvin + "]");
                        var cat = proxy.CategoriesFind(snaps.Content[i].Bvin);
                        if (cat.Errors.Count > 0)
                        {
                            foreach (var err in cat.Errors)
                            {
                                Console.WriteLine("ERROR: " + err.Code + " " + err.Description);
                            }
                        }
                        else
                        {
                            Console.WriteLine("By Bvin: " + cat.Content.Name + " | " + cat.Content.Description);
                        }

                        var catSlug = proxy.CategoriesFindBySlug(snaps.Content[i].RewriteUrl);
                        if (catSlug.Errors.Count > 0)
                        {
                            foreach (var err in catSlug.Errors)
                            {
                                Console.WriteLine("ERROR: " + err.Code + " " + err.Description);
                            }
                        }
                        else
                        {
                            Console.WriteLine("By Slug: " + cat.Content.Name + " | " + cat.Content.Description);
                        }
                    }
                }
            }

            Console.WriteLine("Done - Press a key to continue");
            Console.ReadKey();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
