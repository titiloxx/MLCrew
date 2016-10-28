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
            string cuchi="";//agregar cuchi al titulo
            string contador1="ninguna";//contador de excepciones
            int actualizar=0; //contador que cada x armas guarde el archivo
            //int guardar = 0; ; //contador que cada x armas guarde el archivo
            HttpClient web2 = new HttpClient();
            HttpClient web = new HttpClient();
            string info2;
            int amigo = 0;
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
            if (checkBox1.Checked== true ){
                info2 = File.ReadAllText(Convert.ToString(openFileDialog1.FileName));
            }
            else
            {
                var info = web.GetStringAsync("https://api.opskins.com/IPricing/GetAllLowestListPrices/v1/?appid=730&format=json_pretty");
                info2 = info.Result;
            }
            var picturess = new List<Parameter>(); //Fin mercadolibre
            Dictionary<string, decimal> armas = new Dictionary<string, decimal>();
            var hola = JObject.Parse(info2);
            var HOLA = (JObject)hola["response"];
            foreach (var item in HOLA)
            {
                armas.Add(item.Key, Convert.ToDecimal(item.Value["price"].ToString()));
            }

            foreach (KeyValuePair<string, decimal> item in armas)
            {
             /* if (guardar == 100)
                {

                    guardar = 0;
                }*/
                if (actualizar == 700) { 
                m.refreshToken(m.RefreshToken);
                actualizar = 0;
                }
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
                    string testDivSelector = "//*[@class=\"content csgo-thumbnail\"]";
                    var node = doc.DocumentNode.SelectSingleNode(testDivSelector).InnerHtml;
                    string[] words = node.Split('\'');
                    string nodo1 = words[3];
                    object tamanho = new { source = nodo1 };
                    object[] attr = { tamanho };
                    string nombre = item.Key;
                    if (item.Key.Contains("\u2605"))
                    {
                        cuchi = "Cuchi";
                    }
                    else
                    {
                        cuchi = "";
                    }
                    nombre= nombre.Replace("\u2605", "");//Le saca las estrellas que hacen ver mal la publicacion
                    nombre=nombre.Replace("\u2122", "");

                    IRestResponse response = m.Post("/items", ps, new { title = "CSGO " +nombre+ " SKIN "+cuchi, category_id = "MLA374211", price = venta, listing_type_id = "gold_pro", currency_id = "ARS", available_quantity = 10, buying_mode = "buy_it_now", condition = "new", description = "<div id=\"body\" ms.pgarea=\"body\" class=\"\"> <div><span style=\"text-decoration: underline; color: #0000ff;\"><span style=\"font-size: xx-large;\"><strong>¡Venta de skins CSGO!<br></strong></span></span><p></p></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"></span></span></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"><strong><br></strong></span></span></div><div></div><img class=\"\" src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\" data-src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\"><noscript>&amp;amp;amp;amp;lt;img src=\"https://mla-s2-p.mlstatic.com/103421-MLA20770097984_062016-C.jpg\" /&amp;amp;amp;amp;gt;</noscript><h2>Requisitos:</h2><ul> <li>Tener en \"publico\" el inventario</li> <li>Tener\"Steam Guard Mobile Authenticator\" activo</li> </ul><h2>Descripción: </h2> <p></p><p></p><p><strong><span style=\"font-size: large;\">Arma:</span> </strong><span style=\"font-size: large;\"><span style=\"color: #ff0000;\"><strong>" + nombre + "</strong></span><br></span></p> <p><strong><span style=\"font-size: large;\">El intercambio se hace a través del intercambio de Steam</span></strong>&nbsp; <span style=\"font-size: large; color: #ff0000;\"></span></p><p></p><p></p> <p><span style=\"font-size: large;color: #067935;\"><strong>Se posee otros estados de esta misma arma , consulte.</strong></span></p> <p><span style=\"font-size: large;color: #da00ff;\"><strong>Tenemos todo tipos de skins!</strong></span></p><span style=\"font-size: x-large;color: #731616;\"><u><strong>Importante: Antes de ofertar consultar stock!</strong></u></span> </div>", video_id = "", warranty = "", pictures = attr });
                    string ab = response.Content;
                    amigo = amigo + 1;
                }
                catch (Exception ex)
                {
                    contador1 = (Convert.ToString(contador));
                    contador = contador + 1;

                }
                actualizar = actualizar + 1;
            }
            label4.Text = Convert.ToString(amigo);
            label2.Text = ("Usted safo: " + contador1 + " veces de que se le corte el programa :)");

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        public void button3_Click(object sender, EventArgs e)
        {
            //Si necesitas abrir un archivo
            
            DialogResult result = openFileDialog1.ShowDialog();
            textBox2.Text=Convert.ToString(openFileDialog1.FileName); 
            if (result == DialogResult.OK)
            { label5.Visible = true;
                textBox2.Visible = true;
                checkBox1.Checked = true;
            }
    }

        private void Publicar_Click(object sender, EventArgs e)
        {
            Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
          try
           {
                string cambiarvida = textBox1.Text;
                m.Authorize(cambiarvida, "http://localhost:3000");
               try
                {
                    string nombre = textBox3.Text;
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    var webRequest = HttpWebRequest.Create("https://www.lootmarket.com/csgo/item/" + nombre);
                    Stream stream = webRequest.GetResponse().GetResponseStream();
                    doc.Load(stream);
                    stream.Close();
                    string testDivSelector = "//*[@class=\"content csgo-thumbnail\"]";
                    var node = doc.DocumentNode.SelectSingleNode(testDivSelector).InnerHtml;
                    string[] words = node.Split('\'');
                    string nodo1 = words[3];
                    object tamanho = new { source = nodo1 };
                    object[] attr = { tamanho };
                    var picturess = new List<Parameter>(); //Fin mercadolibre
                    var p = new Parameter();
                    var ps = new List<Parameter>();
                    p.Name = "access_token";
                    p.Value = m.AccessToken;
                    ps.Add(p);
                    string cuchi;
                    if (nombre.Contains("\u2605"))
                    {
                        cuchi = "Cuchi";
                    }
                    else
                    {
                        cuchi = "";
                    }
                    nombre = nombre.Replace("\u2605", "");//Le saca las estrellas que hacen ver mal la publicacion
                    nombre = nombre.Replace("\u2122", "");
                    IRestResponse response = m.Post("/items", ps, new { title = "CSGO " + nombre + " SKIN " + cuchi, category_id = "MLA374211", price = Convert.ToInt32(textBox4.Text), listing_type_id = "free", currency_id = "ARS", available_quantity = 1, buying_mode = "buy_it_now", condition = "used", description = "<div id=\"body\" ms.pgarea=\"body\" class=\"\"> <div><span style=\"text-decoration: underline; color: #0000ff;\"><span style=\"font-size: xx-large;\"><strong>¡Venta de skins CSGO!<br></strong></span></span><p></p></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"></span></span></div><div><span style=\"text-decoration: underline; color: #ff0000;\"><span style=\"font-size: xx-large;\"><strong><br></strong></span></span></div><div></div><img class=\"\" src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\" data-src=\"http://www.csgopools.com/wp-content/uploads/2015/03/cs-go-skincollection.png\"><noscript>&amp;amp;amp;amp;lt;img src=\"https://mla-s2-p.mlstatic.com/103421-MLA20770097984_062016-C.jpg\" /&amp;amp;amp;amp;gt;</noscript><h2>Requisitos:</h2><ul> <li>Tener en \"publico\" el inventario</li> <li>Tener\"Steam Guard Mobile Authenticator\" activo</li> </ul><h2>Descripción: </h2> <p></p><p></p><p><strong><span style=\"font-size: large;\">Arma:</span> </strong><span style=\"font-size: large;\"><span style=\"color: #ff0000;\"><strong>" + nombre + "</strong></span><br></span></p> <p><strong><span style=\"font-size: large;\">El intercambio se hace a través del intercambio de Steam</span></strong>&nbsp; <span style=\"font-size: large; color: #ff0000;\"></span></p><p></p><p></p> <p><span style=\"font-size: large;color: #067935;\"><strong>Se posee otros estados de esta misma arma , consulte.</strong></span></p> <p><span style=\"font-size: large;color: #da00ff;\"><strong>Tenemos todo tipos de skins!</strong></span></p><span style=\"font-size: x-large;color: #731616;\"><u><strong>Importante: Antes de ofertar consultar stock!</strong></u></span> </div>", video_id = "", warranty = "", pictures = attr });
                    var hola = JObject.Parse(response.Content);
                    var HOLA = (JValue)hola["permalink"];
                     Clipboard.SetText(Convert.ToString(HOLA));
                    MessageBox.Show("Arma publicada con exito y link copiado al clipboard!");
               }


                catch
                {
                    MessageBox.Show("Arma invalida, revise el texto ingresado");
                }
           }
           catch
            {
                MessageBox.Show("Pone el codigo en la otra ventana!!");
          }
         
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}



