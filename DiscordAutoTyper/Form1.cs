using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordAutoTyper
{
    public partial class Form1 : Form
    {

        /*
         * This is a program developed by Klives, to automate xp farming on discord servers.
         * This works as in simulating keyboard typing, it does not interact with the discord application files.
         */

        // To send keys:  SendKeys.Send("{ENTER}");


        public Form1()
        {
            InitializeComponent();
        }

        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        bool ocassional = false;

        // The three mouse events are to move the the window by clicking onto the form.
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        //The button below is to quit the application.
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        // Start loop.
        private void button1_Click(object sender, EventArgs e)
        {
            if (chancebox.Text == string.Empty)
            {
                ocassional = false;
            }
            else
            {
                ocassional = true;
            }
            // Terrible practice below, but.... who cares?
            bool isNumeric = true;
            foreach (char c in delaytime.Text)
            {
                if (!Char.IsNumber(c))
                {
                    isNumeric = false;
                    break;
                }
            }
            if (isNumeric == false)
            {
                MessageBox.Show("Delay time must be a numeric value.");
            }
            else
            {
                Task delay = Task.Delay(4000);
                delay.Wait();
                StartLoop();
            }
        }

        void StartLoop()
        {
            try
            {
                if (ocassional)
                {
                    // I know this isnt the best way to do this, but who cares.
                    Random rnd = new Random();
                    int decider = rnd.Next(1, 10);
                    if (decider == 5 || decider == 8)
                    {
                        char[] characters = chancebox.Text.ToCharArray();
                        foreach (char character in characters)
                        {
                            SendKeys.Send("{" + character.ToString() + "}");
                        }
                        SendKeys.Send("{ENTER}");
                    }
                    else
                    {
                        char[] characters = typer.Text.ToCharArray();
                        foreach (char character in characters)
                        {
                            SendKeys.Send("{" + character.ToString() + "}");
                        }
                        SendKeys.Send("{ENTER}");
                    }
                }
                else
                {
                    char[] characters = typer.Text.ToCharArray();
                    foreach (char character in characters)
                    {
                        SendKeys.Send("{" + character.ToString() + "}");
                    }
                    SendKeys.Send("{ENTER}");
                }
                StringToInt sti = new StringToInt();
                int? time = sti.ConvertStringToInt(delaytime.Text);
                //MessageBox.Show(time.Value.ToString()); Debug
                Task delayy = Task.Delay(time.Value);
                delayy.Wait();
                StartLoop();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Since auto delete isn't a feature yet, this isn't functional, shows a message box when clicked.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MessageBox.Show("This feature isn't available.");
                checkBox1.Checked = false;
            }
        }
    }
}
