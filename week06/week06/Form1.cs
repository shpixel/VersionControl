using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week06.Entities;

namespace week06
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {

            InitializeComponent();

            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Ball newball = Factory.CreateNew();
            _balls.Add(newball);
            mainPanel.Controls.Add(newball);
            newball.Left = -newball.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int maxx = 0;
            Ball selball;

            foreach (var item in _balls)
            {
                item.MoveBall();
                if (item.Left > maxx)
                {
                    maxx = item.Left;
                }
            }

            if (maxx >= 1000)
            {
                selball = _balls[0];
                _balls.Remove(selball);
                mainPanel.Controls.Remove(selball);
            }

        }
    }
}
