﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{

    public partial class Form1 : Form
    {
        public Guid selectedId = Guid.Empty;

        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.FullName;

            button2.Text = Resource1.WriteToFile;

            listBox1.DisplayMember = "FullName";
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                FullName = textBox1.Text
            };

            users.Add(user);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                {
                    foreach (var name in users)
                    {
                        string sw = name.ID.ToString();
                        sw += ",";
                        sw += name.FullName;
                        file.WriteLine(sw);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue == null) return;
            
            selectedId = (Guid)listBox1.SelectedValue;
            ;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedId == Guid.Empty) return;

            foreach (var user in users)
            {
                if (user.ID == selectedId)
                {
                    users.Remove(user);
                    break;
                }
            }
            if(listBox1.SelectedValue!=null) selectedId = (Guid)listBox1.SelectedValue;

        }
    }
}
