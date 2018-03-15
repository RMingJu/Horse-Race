using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace 賽馬
{
    public partial class Form1 : Form
    {
        Label[] horses;
        bool _IsOver = false;
        Label goal;
        int _horseCount;

        public Form1()
        {
            InitializeComponent();
            Image ii = 賽馬.Properties.Resources.Goal_Small;
            goal = new Label();
            goal.Text = "";
            goal.Image = ii;
            goal.Size = ii.Size;
            goal.Location = new Point(450,20);
            this.Controls.Add(goal);
            label7.Text = goal.Location.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                _horseCount = int.Parse(comboBox1.Text);
                horses = new Label[_horseCount];
                Image ii = 賽馬.Properties.Resources.Horse_Small;
                for (int i = 0; i < _horseCount; i++)
                {
                    horses[i] = new Label();
                    horses[i].Text = "";
                    horses[i].Tag = "馬" + (i + 1);
                    horses[i].Image = ii;
                    horses[i].Size = ii.Size;
                    horses[i].Location = new Point(50, 150 + 60 * (i % _horseCount));
                }

                foreach (var x in horses)
                {
                    this.Controls.Add(x);
                }
            }
            catch (Exception ex) { }

        }

        private async void   button2_Click(object sender, EventArgs e)
        {
            int x = label1.Left;
            Random random = new Random();
            while (!_IsOver)
            {
                
                await Task.Run(() =>
                {

                    x += random.Next(1,30);
                    Thread.Sleep(100);
                });

                label1.Left = x;
                
                if (label1.Location.X >= goal.Location.X)
                {
                    label1.Text = label1.Location.ToString();
                    break;
                }
            }
            
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            
            while (!_IsOver)
            {
                int run = await HorseRun();
                label1.Left += run;
                if (label1.Location.X >= goal.Location.X)
                {
                    label1.Text = label1.Location.ToString();
                    _IsOver = true;
                }
            }
            
        }
        async Task<int> HorseRun()
        {
            Random random = new Random();
            int r = random.Next(1, 50);
            await Task.Delay(25);
            return r;
        }
        
       
        private String WhoIsWinner()
        {
            foreach (var x in horses)
            {
                if (x.Location.X >= goal.Location.X)
                {
                    _IsOver = true;
                    return x.Tag + " 贏了!";
                }
            }
                    return "等待結果...";
        }

       

        private async void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "計算中......";

            long newx = await Task.Run<long>(()=> 
            {
                long ans = 0;
                for (long i = 1; i <= 10000000000; i++)
                {
                    ans += i;
                }
                return ans;
            });

            label1.Text = newx.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(Mycalc);
            Thread th = new Thread(ts);
            th.Start();
        }
        void Mycalc()
        {
            long ans = 0;
            for (long i = 1; i <= 100000; i++)
            {
                ans += i;
                Real_mycalc(ans.ToString());    
            }
            
        }
        delegate void mycalc(string total);
        void Real_mycalc(string total_ans)
        {
            if (label1.InvokeRequired)
            {
                mycalc mc = new mycalc(Real_mycalc);
                this.Invoke(mc, total_ans);
            }
            label1.Text = total_ans.ToString();
            //Thread.Sleep(1000);
        }

        private async void button6_Click(object sender, EventArgs e)
        {

            while (!_IsOver)
            {
                try
                {
                    int run = await HorseRun();
                    int run2 = await HorseRun();
                    int run3 = await HorseRun();
                    int run4 = await HorseRun();
                    int run5 = await HorseRun();
                    int run6 = await HorseRun();
                    horses[0].Left += run;
                    horses[1].Left += run2;
                    horses[2].Left += run3;
                    horses[3].Left += run4;
                    horses[4].Left += run5;
                    horses[5].Left += run;
                    
                }
                catch (Exception ex) { }
                finally { label1.Text = WhoIsWinner(); }
                

                
                
            }
        }
    }
}
