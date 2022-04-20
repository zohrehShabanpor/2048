using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game_2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int TEDAD = 4, SPACE = 10;
        Label[,] safhe = new Label[TEDAD, TEDAD];
        Color DefColor = Color.FromArgb(204, 192, 180);
        bool HasMoved = false;
        int DefRandomNum;
        private void Form1_Load(object sender, EventArgs e)
        {
            int w = (this.ClientSize.Width - (TEDAD + 1) * SPACE) / TEDAD;
            int h = (this.ClientSize.Height - (TEDAD + 1) * SPACE) / TEDAD;
            CreatTable(w, h);
            RandomNumber();
            RandomNumber();
        }

        private void CreatTable(int w, int h)
        {
            for (int i = 0; i < TEDAD; i++)
            {
                for (int j = 0; j < TEDAD; j++)
                {
                    Label l = new Label();
                    l.AutoSize = false;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Size = new Size(w, h);
                    l.Left = j * w + (j + 1) * SPACE;
                    l.Top = i * h + (i + 1) * SPACE;
                    l.Font = new Font(l.Font.FontFamily, 25);
                    l.BackColor = DefColor;
                    this.Controls.Add(l);
                    safhe[i, j] = l;
                }
            }
        }

        private void RandomNumber()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            DefRandomNum = r.Next(1, 11) <= 8 ? 2 : 4;
            int row, col;
            do
            {
                row = r.Next(0, TEDAD);
                col = r.Next(0, TEDAD);
            }
            while (safhe[row, col].Text != "");
            safhe[row, col].Text = DefRandomNum.ToString();
            Coloring(DefRandomNum, row, col);
        }

        private void Coloring(int a, int row, int col)
        {
            Color[] colors = { Color.FromArgb(240,228,218), Color.FromArgb(237,225,200), Color.FromArgb(242, 178, 122)
                , Color.FromArgb(234,141,81), Color.FromArgb(245, 124, 99), Color.FromArgb(232, 89, 59),  Color.FromArgb(237, 206, 115)
            ,  Color.FromArgb(235, 202, 99) ,  Color.FromArgb(238, 199, 80) , Color.FromArgb(236, 196, 64)
             , Color.FromArgb(238, 194, 46) , Color.FromArgb(255, 60, 62) , Color.FromArgb(255, 30, 32) 
             , Color.FromArgb(255, 29, 31)};
            safhe[row, col].BackColor = colors[(int)Math.Log(a, 2) - 1];
            foreach (Label item in this.Controls.OfType<Label>())
            {
                if (item.Text == string.Empty)
                {
                    item.BackColor = DefColor;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    ShiftToLeft();
                    for (int row = 0; row < TEDAD; row++)
                    {
                        for (int col2 = 0; col2 < TEDAD; col2++)
                        {
                            if (col2 != 3 && safhe[row, col2].Text == safhe[row, col2 + 1].Text
                                && safhe[row, col2].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row, col2].Text) * 2;
                                safhe[row, col2].Text = Convert.ToString(sum1);
                                safhe[row, col2 + 1].Text = string.Empty;
                                Coloring(sum1, row, col2);
                                if (col2 < TEDAD - 1)
                                    col2++;
                            }
                            if (col2 != 0 && safhe[row, col2].Text == safhe[row, col2 - 1].Text
                                && safhe[row, col2].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row, col2].Text) * 2;
                                safhe[row, col2 - 1].Text = Convert.ToString(sum1);
                                safhe[row, col2].Text = string.Empty;
                                Coloring(sum1, row, col2);
                                if (col2 < TEDAD - 1)
                                    col2++;
                            }
                        }
                    }
                    ShiftToLeft();
                    if (HasMoved)
                        RandomNumber();
                    HasMoved = false;
                    break;
                case Keys.Right:
                    ShiftToRight();
                    for (int row = 0; row < TEDAD; row++)
                    {
                        for (int col2 = (TEDAD - 1); col2 >= 0; col2--)
                        {
                            if (col2 != 3 && safhe[row, col2].Text == safhe[row, col2 + 1].Text
                                && safhe[row, col2].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row, col2].Text) * 2;
                                safhe[row, col2 + 1].Text = Convert.ToString(sum1);
                                safhe[row, col2].Text = string.Empty;
                                Coloring(sum1, row, col2 + 1);
                                if (col2 > 1)
                                    col2--;

                            }
                            if (col2 != 0 && safhe[row, col2].Text == safhe[row, col2 - 1].Text
                                && safhe[row, col2].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row, col2].Text) * 2;
                                safhe[row, col2].Text = Convert.ToString(sum1);
                                safhe[row, col2 - 1].Text = string.Empty;
                                Coloring(sum1, row, col2);
                                if (col2 > 1)
                                    col2--;
                            }
                        }
                    }
                    ShiftToRight();
                    if (HasMoved)
                        RandomNumber();
                    HasMoved = false;
                    break;
                case Keys.Down:
                    ShiftToDown();
                    for (int col = 0; col < TEDAD; col++)
                    {
                        for (int row2 = (TEDAD - 1); row2 >= 0; row2--)
                        {
                            if (row2 != 3 && safhe[row2, col].Text == safhe[row2 + 1, col].Text
                                && safhe[row2, col].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row2, col].Text) * 2;
                                safhe[row2 + 1, col].Text = Convert.ToString(sum1);
                                safhe[row2, col].Text = string.Empty;
                                Coloring(sum1, row2 + 1, col);
                                if (row2 > 1)
                                    row2--;
                            }
                            if (row2 != 0 && safhe[row2, col].Text == safhe[row2 - 1, col].Text
                                && safhe[row2, col].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row2, col].Text) * 2;
                                safhe[row2, col].Text = Convert.ToString(sum1);
                                safhe[row2 - 1, col].Text = string.Empty;
                                Coloring(sum1, row2, col);
                                if (row2 > 1)
                                    row2--;
                            }
                        }
                    }
                    ShiftToDown();
                    if (HasMoved)
                        RandomNumber();
                    HasMoved = false;
                    break;
                case Keys.Up:
                    ShiftToUp();
                    for (int col = 0; col < TEDAD; col++)
                    {
                        for (int row2 = 0; row2 < TEDAD; row2++)
                        {
                            if (row2 != 3 && safhe[row2, col].Text == safhe[row2 + 1, col].Text
                                && safhe[row2, col].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row2, col].Text) * 2;
                                safhe[row2, col].Text = Convert.ToString(sum1);
                                safhe[row2 + 1, col].Text = string.Empty;
                                Coloring(sum1, row2, col);
                                if (row2 < TEDAD - 1)
                                    row2++;
                            }
                            if (row2 != 0 && safhe[row2, col].Text == safhe[row2 - 1, col].Text
                                && safhe[row2, col].Text != "")
                            {
                                int sum1 = Convert.ToInt32(safhe[row2, col].Text) * 2;
                                safhe[row2 - 1, col].Text = Convert.ToString(sum1);
                                safhe[row2, col].Text = string.Empty;
                                Coloring(sum1, row2 - 1, col);
                                if (row2 < TEDAD - 1)
                                    row2++;
                            }
                        }
                    }
                    ShiftToUp();
                    if (HasMoved)
                        RandomNumber();
                    HasMoved = false;
                    break;
                case Keys.F2:
                    HasMoved = false;
                    foreach (Label item in this.Controls)
                    {
                        item.Text = "";
                        item.BackColor = DefColor;
                    }
                    RandomNumber();
                    RandomNumber();
                    break;
            }
        }

        private void ShiftToUp()
        {
            for (int col = 0; col < TEDAD; col++)
            {
                int first_emptyrow = -1;
                for (int row = 0; row < TEDAD; row++)
                {
                    if (safhe[row, col].Text == "")
                    {
                        if (first_emptyrow == -1)
                            first_emptyrow = row;
                    }
                    else
                    {
                        if (first_emptyrow != -1)
                        {
                            safhe[first_emptyrow, col].Text = safhe[row, col].Text;
                            HasMoved = true;
                            safhe[first_emptyrow, col].BackColor = safhe[row, col].BackColor;
                            safhe[row, col].Text = "";
                            safhe[row, col].BackColor = DefColor;
                            row = first_emptyrow;
                            first_emptyrow = -1;
                        }
                    }
                }
            }
        }

        private void ShiftToDown()
        {
            for (int col = 0; col < TEDAD; col++)
            {
                int first_emptyrow = 4;
                for (int row = (TEDAD - 1); row >= 0; row--)
                {
                    if (safhe[row, col].Text == "")
                    {
                        if (first_emptyrow == 4)
                            first_emptyrow = row;
                    }
                    else
                    {
                        if (first_emptyrow != 4)
                        {
                            safhe[first_emptyrow, col].Text = safhe[row, col].Text;
                            HasMoved = true;
                            safhe[first_emptyrow, col].BackColor = safhe[row, col].BackColor;
                            safhe[row, col].Text = "";
                            safhe[row, col].BackColor = DefColor;
                            row = first_emptyrow;
                            first_emptyrow = 4;
                        }
                    }
                }
            }
        }

        private void ShiftToRight()
        {
            for (int row = 0; row < TEDAD; row++)
            {
                int first_emptyCol = 4;
                for (int col = (TEDAD - 1); col >= 0; col--)
                {
                    if (safhe[row, col].Text == "")
                    {
                        if (first_emptyCol == 4)
                            first_emptyCol = col;
                    }
                    else
                    {
                        if (first_emptyCol != 4)
                        {
                            safhe[row, first_emptyCol].Text = safhe[row, col].Text;
                            HasMoved = true;
                            safhe[row, first_emptyCol].BackColor = safhe[row, col].BackColor;
                            safhe[row, col].Text = "";
                            safhe[row, col].BackColor = DefColor;
                            col = first_emptyCol;
                            first_emptyCol = 4;
                        }
                    }
                }
            }
        }

        private void ShiftToLeft()
        {
            for (int row = 0; row < TEDAD; row++)
            {
                int first_emptyCol = -1;
                for (int col = 0; col < TEDAD; col++)
                {
                    if (safhe[row, col].Text == "")
                    {
                        if (first_emptyCol == -1)
                            first_emptyCol = col;
                    }
                    else
                    {
                        if (first_emptyCol != -1)
                        {
                            safhe[row, first_emptyCol].Text = safhe[row, col].Text;
                            HasMoved = true;
                            safhe[row, first_emptyCol].BackColor = safhe[row, col].BackColor;
                            safhe[row, col].Text = "";
                            safhe[row, col].BackColor = DefColor;
                            col = first_emptyCol;
                            first_emptyCol = -1;
                        }
                    }
                }
            }
        }
    }
}
