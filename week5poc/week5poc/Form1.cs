using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using week5poc.Entities;
using week5poc.MnbServiceReference;

namespace week5poc
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates;
            comboBox1.DataSource = Currencies;
            FillCurrencies();
            RefreshData();
        }

        public void FillCurrencies()
        {
            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(response.GetCurrenciesResult);

            foreach (XmlElement item in xml.DocumentElement)
            {
                foreach (XmlElement i2 in item)
                {
                    string newCurrency;
                    newCurrency = i2.InnerText;
                    Currencies.Add(newCurrency);
                }
            }
            ;
        }

        public string GetRates()
        {
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();

            request.currencyNames = (string)comboBox1.SelectedItem;
            request.startDate = dateTimePicker1.Value.ToString();
            request.endDate = dateTimePicker2.Value.ToString();

            GetExchangeRatesResponseBody response = mnbService.GetExchangeRates(request);

            string result = response.GetExchangeRatesResult;

            return result;
        }

        public void Feldolgozas(string ixml)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(ixml);

            foreach (XmlElement item in xml.DocumentElement)
            {

                RateData newdata = new RateData();
                Rates.Add(newdata);

                newdata.Date = DateTime.Parse(item.GetAttribute("date"));

                /*foreach (XmlElement i2 in item) {
                    newdata.Currency = i2.GetAttribute("curr");
                    var unit = decimal.Parse(i2.GetAttribute("unit"));
                    var value = decimal.Parse(i2.InnerText);

                    if (unit != 0)
                    {
                        newdata.Value = value / unit / 100;

                    }
                }*/

                XmlElement child = (XmlElement)item.ChildNodes[0];
                if (child == null) continue;

                newdata.Currency = child.GetAttribute("curr");


                var unit = decimal.Parse(child.GetAttribute("unit"));
                var value = decimal.Parse(child.InnerText);

                if (unit != 0)
                {
                    newdata.Value = value / unit / 100;

                }
            }
        }

        public void MakeChart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var ca = chartRateData.ChartAreas[0];
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            ca.AxisY.IsStartedFromZero = false;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;


        }

        public void RefreshData()
        {
            Rates.Clear();

            string rates = GetRates();
            Feldolgozas(rates);
            MakeChart();
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
    }
}
