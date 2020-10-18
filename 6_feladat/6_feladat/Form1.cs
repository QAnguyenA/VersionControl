using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _6_feladat.Entities;
using _6_feladat.MnbServiceReference;

namespace _6_feladat
{
    public partial class Form1 : Form
    {

        List<RateData> Rates;
        public Form1()
        {
            InitializeComponent();
            Hivas();

            dataGridView1.DataSource = Rates;
        }

        private void Hivas()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw= new StreamWriter(sfd.FileName,false, Encoding.UTF8))
            {
                foreach (var a in result)
                {
                    sw.Write(a);
                }
            }

        }
    }
}
