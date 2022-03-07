using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_IM
{
    public partial class Form1 : Form
    {
        private int rows = 1, columns = 15;
        private bool canEdit = true;
        private char[] rule = new char[8];
        private string[] Pattern = new string[] { 
            "111", "110", "101", "100", "011", "010", "001", "000" };

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < columns; i++)
            {
                dataGridView1.Columns.Add("", "");
            }
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = rows.ToString();

            setrule(Convert.ToString((int)edRule.Value, 2));
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void edRule_ValueChanged(object sender, EventArgs e)
        {
            setrule(Convert.ToString((int)edRule.Value, 2));
        }

        private void setrule(string props)
        {
            int l = props.Length;
            while (l < 8)
            {
                props = "0" + props;
                l = props.Length;
            }
            for (int i = 0; i < props.Length; i++)
            {
                rule[i] = props[i];
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            char[] oldRow = new char[columns];
            char[] newRow = new char[columns];

            for (int i = 0; i < columns; i++)
            {
                if (dataGridView1[i, rows - 1].Style.BackColor == Color.ForestGreen) oldRow[i] = '1';
                else oldRow[i] = '0';
            }
            
            for (int i = 0; i < columns; i++)
            {
                string pattern;
                int prevNum = i - 1;
                int nextNum = i + 1;
                var builder = new StringBuilder();

                if (i == 0) { 
                    prevNum = columns - 1; 
                }
                if (i == columns - 1) { 
                    nextNum = 0; 
                }

                builder.Append(oldRow[prevNum]);
                builder.Append(oldRow[i]);
                builder.Append(oldRow[nextNum]);

                pattern = builder.ToString();

                int index = Array.IndexOf(Pattern, pattern);
                newRow[i] = rule[index];

            }

            rows++;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[rows - 1].HeaderCell.Value = rows.ToString();

            for (int i = 0; i < columns; i++)
            {
                if (newRow[i] == '1')  
                    dataGridView1[i, rows-1].Style.BackColor = Color.ForestGreen;
                else 
                    dataGridView1[i, rows-1].Style.BackColor = Color.White;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (canEdit)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.ForestGreen;
                        dataGridView1.ClearSelection();
                        break;
                    case MouseButtons.Right:
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                        dataGridView1.ClearSelection();
                        break;
                }
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            canEdit = false;
            timer1.Stop();
        }
    }
}
