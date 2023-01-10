using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Models;
using Newtonsoft.Json;
namespace Client
{
    public partial class Form1 : Form
    {
        HttpClient httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception)
            {

               
            }
           
        }
         async void LoadData()
        {
            var ProductJson = await httpClient.GetStringAsync("Home/Products");
            List<Products> products = JsonConvert.DeserializeObject<List<Products>>(ProductJson);
            var RassrochkaJson = await httpClient.GetStringAsync("Home/Rassrochki");
            List<int> rassrochka = JsonConvert.DeserializeObject<List<int>>(RassrochkaJson);
            comboBox1.DataSource = products;
            comboBox1.DisplayMember = "Name";
            comboBox2.DataSource = rassrochka;
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            httpClient.BaseAddress = new Uri(textBox1.Text);
            try
            {
                LoadData();
            }
            catch (Exception)
            {

               
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            List<Operatsiya> operatsiyas = new List<Operatsiya>();
            operatsiyas.Add(
                 new Operatsiya
                 {
                     PhoneNumber = textBox2.Text,
                     howMonth = int.Parse(comboBox2.Text),
                     idproduct = ((Products)comboBox1.SelectedItem).id,
                     CountProduct =int.Parse(textBox3.Text)
                 }
                ) ;

            var JsonObj = JsonConvert.SerializeObject(operatsiyas);
            var buffer = System.Text.Encoding.UTF8.GetBytes(JsonObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await httpClient.PostAsync("Home/GetData",byteContent);
            if (result.IsSuccessStatusCode)
            {
                result.EnsureSuccessStatusCode();
                var OtchetJson = await result.Content.ReadAsStringAsync();
               List< Otchet> _ot = JsonConvert.DeserializeObject<List<Otchet>>(OtchetJson);
                Otchet _otchet = _ot.Last();
                richTextBox1.Text = " Шумо " + _otchet.countProduct + "адад " + _otchet.ProductName + " ба насия ба мухлати " + Environment.NewLine + _otchet.Month + " мох ба маблаги  " + _otchet.Summa + " сомони харидори намудед. ";
            }
        }
    }
}
