﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;


namespace FormFindCalls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
         */
        private void button1_Click(object sender, EventArgs e)
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RBC_call_path;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand query = new SqlCommand("select * from rbc_contacts", con);
            con.Open();
            SqlDataReader reader = query.ExecuteReader();

            lbl_paths.Text += "\n";

            while (reader.Read())
            {
                string x = reader.GetString(1);//audio_module_no
                string y = reader.GetString(2);//audio_channel_no
                int L = (x + y).Length;//combined length of above two
                int howManyZeros = (L != 15) ? 15-L : 0;//if x+y is 15 then do NOT insert any zeros. 
                string zeros = "";
                while(howManyZeros != 0)//add string of zeros to be inserted between x & y
                {
                    zeros += "0";
                    howManyZeros--;
                }
                y = zeros + y;
                    

                lbl_paths.Text += x + y + "\n";
                lbl_paths.Text += "Path is: \\\\SE441903.maple.fg.rbc.com\\h$\\Calls\\" +
                    x +
                    "\\" + y.Substring(0, 3) +
                    "\\" + y.Substring(3, 2) +
                    "\\" + y.Substring(5, 2) +
                    "\\" + y.Substring(7, 2) +
                    "\n\n";
            }
            con.Close();
            
        }
    }
}
