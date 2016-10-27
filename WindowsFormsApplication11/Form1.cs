using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using RestSharp;
using System.Diagnostics;
using MercadoLibre.SDK;
using System.IO;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
            string redirectUrl = m.GetAuthUrl(Meli.AuthUrls.MLA, "http://localhost:3000");
            Process.Start(redirectUrl);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int amigo = 1;
            Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
            string cambiarvida = textBox1.Text;
            int contador = 1;
            m.Authorize(cambiarvida, "http://localhost:3000");
            Dictionary<string, string> abb = new Dictionary<string, string>();
            var p = new Parameter();
            var ps = new List<Parameter>();
            p.Name = "access_token";
            p.Value = m.AccessToken;
            ps.Add(p);
            string info2 = File.ReadAllText(@"op.txt");
            var picturess = new List<Parameter>(); //Fin mercadolibre
            HttpClient web2 = new HttpClient();
            Dictionary<string, decimal> armas = new Dictionary<string, decimal>();
            HttpClient web = new HttpClient();
           // var info = web.GetStringAsync("https://api.opskins.com/IPricing/GetAllLowestListPrices/v1/?appid=730&format=json_pretty");
            var hola = JObject.Parse(info2);
            var HOLA = (JObject)hola["response"];
            foreach (var item in HOLA)
            {
                armas.Add(item.Key, Convert.ToDecimal(item.Value["price"].ToString()));
            }

            foreach (KeyValuePair<string, decimal> item in armas)
            {
                m.refreshToken();
                decimal precio = item.Value / 100;
                decimal venta;
                int argentino = Convert.ToInt32(precio * 16);
                int profit2 = 0;
                if (precio <= 1 && precio > 0)
                {
                    profit2 = 20;
                }
                if (precio <= 5 && precio > 1)
                {
                    profit2 = 40;
                }
                if (precio <= 10 && precio > 5)
                {
                    profit2 = 70;
                }
                if (precio <= 15 && precio > 10)
                {
                    profit2 = 75;
                }
                if (precio <= 20 && precio > 15)
                {
                    profit2 = 90;
                }
                if (precio <= 25 && precio > 20)
                {
                    profit2 = 160;
                }
                if (precio <= 30 && precio > 25)
                {
                    profit2 = 200;
                }
                if (precio <= 35 && precio > 30)
                {
                    profit2 = 225;
                }
                if (precio <= 45 && precio > 35)
                {
                    profit2 = 250;
                }
                if (precio <= 55 && precio > 45)
                {
                    profit2 = 310;
                }
                if (precio <= 65 && precio > 55)
                {
                    profit2 = 350;
                }
                if (precio <= 75 && precio > 65)
                {
                    profit2 = 370;
                }
                if (precio <= 85 && precio > 75)
                {
                    profit2 = 410;
                }
                if (precio <= 95 && precio > 85)
                {
                    profit2 = 450;
                }
                if (precio <= 105 && precio > 95)
                {
                    profit2 = 465;
                }
                if (precio <= 105 && precio > 95)
                {
                    profit2 = 500;
                }
                if (precio <= 125 && precio > 105)
                {
                    profit2 = 550;
                }
                if (precio <= 155 && precio > 125)
                {
                    profit2 = 630;
                }
                if (precio <= 175 && precio > 155)
                {
                    profit2 = 700;
                }
                if (precio <= 200 && precio > 175)
                {
                    profit2 = 770;
                }
                if (precio <= 250 && precio > 200)
                {
                    profit2 = 850;
                }
                if (precio <= 300 && precio > 250)
                {
                    profit2 = 1100;
                }
                if (precio <= 350 && precio > 300)
                {
                    profit2 = 1400;
                }
                if (precio <= 400 && precio > 350)
                {
                    profit2 = 1600;
                }
                if (precio <= 450 && precio > 400)
                {
                    profit2 = 1800;
                }
                if (precio <= 500 && precio > 450)
                {
                    profit2 = 1900;
                }
                if (precio <= 550 && precio > 500)
                {
                    profit2 = 2000;
                }
                if (precio <= 600 && precio > 550)
                {
                    profit2 = 2100;
                }
                if (precio <= 700 && precio > 600)
                {
                    profit2 = 2200;
                }
                if (precio <= 800 && precio > 700)
                {
                    profit2 = 2300;
                }
                if (precio > 800)
                {
                    profit2 = 2500;
                }
                venta = argentino + profit2;
                HttpClient web3 = new HttpClient();
                // Call the page and get the generated HTML
                var doc = new HtmlAgilityPack.HtmlDocument();
                HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                doc.OptionWriteEmptyNodes = true;
                try
                {
                    var webRequest = HttpWebRequest.Create("https://www.lootmarket.com/csgo/item/" + item.Key);
                    Stream stream = webRequest.GetResponse().GetResponseStream();
                    doc.Load(stream);
                    stream.Close();
                    //*[@id="mainContents"]/div[2]/div/div[1]/img
                    //get the div by id and then get the inner text 
                    //string testDivSelector = "//*[@id=\"mainContents\"]//div//div/div//img";
                    string testDivSelector = "//*[@class=\"content csgo-thumbnail\"]";
                    var node = doc.DocumentNode.SelectSingleNode(testDivSelector).InnerHtml;
                    string[] words = node.Split('\'');

                    //*[@id="mainContents"]/div[2]/div/div[1]/img
                    //get the div by id and then get the inner text 
                    //string testDivSelector = "//*[@id=\"mainContents\"]//div//div/div//img";
                    //var text = web3.GetStringAsync("http://csgobackpack.net/api/GetItemPrice/?currency=USD&time=7&icon=1&id=" + item.Key);
                    //var steam = JsonConvert.DeserializeObject<JObject>(text.Result);
                    //string pic = Convert.ToString(steam["icon"]);
                    string nodo1 = words[3];
                    object tamanho = new { source = nodo1 };
                    object[] attr = { tamanho };
                    IRestResponse response = m.Post("/items", ps, new { title = "Skin " + item.Key + " CsGo ", category_id = "MLA374211", price = venta, listing_type_id = "gold_pro", currency_id = "ARS", available_quantity = 10, buying_mode = "buy_it_now", condition = "new", description = "<div id=\"body\" ms.pgarea=\"body\" class=\"\"> <div><span style=\"text-decoration: underline; color: #0000ff;\"><span style=\"font-size: xx-large;\"><strong>¡Venta de skins CSGO!<br></strong></span></span><p></p></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"></span></span></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"><strong><br></strong></span></span></div><div></div><img class=\"\" src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\" data-src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\"><noscript>&amp;amp;amp;amp;lt;img src=\"https://mla-s2-p.mlstatic.com/103421-MLA20770097984_062016-C.jpg\" /&amp;amp;amp;amp;gt;</noscript><h2>Requisitos:</h2><ul> <li>Tener en \"publico\" el inventario</li> <li>Tener\"Steam Guard Mobile Authenticator\" activo</li> </ul><h2>Descripción: </h2> <p></p><p></p><p><strong><span style=\"font-size: large;\">Arma:</span> </strong><span style=\"font-size: large;\"><span style=\"color: #ff0000;\"><strong>" + item.Key + "</strong></span><br></span></p> <p><strong><span style=\"font-size: large;\">El intercambio se hace a través del intercambio de Steam</span></strong>&nbsp; <span style=\"font-size: large; color: #ff0000;\"></span></p><p></p><p></p> <p><span style=\"font-size: large;color: #067935;\"><strong>Se posee otros estados de esta misma arma , consulte.</strong></span></p> <p><span style=\"font-size: large;color: #da00ff;\"><strong>Tenemos todo tipos de skins!</strong></span></p><span style=\"font-size: x-large;color: #731616;\"><u><strong>Importante: Antes de ofertar consultar stock!</strong></u></span> </div>", video_id = "", warranty = "", pictures = attr });
                    string ab = response.Content;
                    label4.Text = Convert.ToString(amigo);
                    amigo = amigo + 1;
                }
                catch (Exception ex)
                {
                    string contador1 = (Convert.ToString(contador));
                   label2.Text = ("Usted safo: "+ contador1 + " veces de que se le corte el programa :)");
                    contador = contador + 1;

                }
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
    }



