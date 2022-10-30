using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week5poc.Entities;
using week5poc.MnbServiceReference;

namespace week5poc
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService=new MNBArfolyamServiceSoapClient();
        BindingList<rateData> RateData = new BindingList<rateData>();

        public Form1()
        {
            InitializeComponent();
            GetRates();

            dataGridView1.DataSource = RateData;
        }

        public void GetRates()
        {
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();

            request.currencyNames = "EUR";
            request.startDate = "2020-01-01";
            request.endDate = "2020-06-30";

            GetExchangeRatesResponseBody response = mnbService.GetExchangeRates(request);

            string result = response.GetExchangeRatesResult;
        }
    }
}
