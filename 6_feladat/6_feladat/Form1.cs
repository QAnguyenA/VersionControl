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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using _6_feladat.Entities;
using _6_feladat.MnbServiceReference;

namespace _6_feladat
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            Getcurrencies();
            RefreshData();
            Chart();
            

            dataGridView1.DataSource = Rates;
            comboBox1.DataSource = Currencies;



        }

        private void RefreshData()
        {
            Rates.Clear();
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            /*SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw= new StreamWriter(sfd.FileName,false, Encoding.UTF8))
            {
                foreach (var a in result)
                {
                    sw.Write(a);
                }
            }
            */

            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");
              

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                {
                    rate.Value = value / unit;
                }
            }
        }

        private void Chart()
        {
            
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

   

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void Getcurrencies()
        {
            var mnbService2 = new MNBArfolyamServiceSoapClient();

            var request2 = new GetCurrenciesRequestBody()
            {
                
            };

            var response2 = mnbService2.GetCurrencies(request2);

            var result = response2.GetCurrenciesResult;

            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                Currencies.Add(result);
               


            }
        


            
        }
    }

        
    
}
