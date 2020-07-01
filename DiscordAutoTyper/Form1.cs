using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
        bool AutoDeleteMessages = false;
        bool stopped = false;


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
                SubLoop();
            }
        }

        public async Task SubLoop()
        {
            stopped = false;
            button4.Visible = true;
            await Task.Delay(4000);
            await StartLoop();
        }

        public async Task StartLoop()
        {
            try
            {
                if (stopped != true)
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
                    //Task delayy = Task.Delay(time.Value * 1000);
                    await Task.Delay(time.Value * 1000);
                    await StartLoop();
                }   
            }
            catch(Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                MessageBox.Show("Error: " + ex.Message+" Line Number: "+line.ToString());
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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int[] CursorPos = { System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y };
                //MessageBox.Show(CursorPos[1].ToString(), ", " + CursorPos[2].ToString());
                MessageBox.Show(System.Windows.Forms.Cursor.Position.X + ", " + System.Windows.Forms.Cursor.Position.Y);
                Cursor.Position = new Point(352, 953);
                
                Cursor.Position = new Point(388, 898);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button3.Visible = true;
            }
            else
            {
                button3.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stopped = true;
            button4.Visible = false;
        }
    }
}
