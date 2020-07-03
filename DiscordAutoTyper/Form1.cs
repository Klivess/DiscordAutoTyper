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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

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
            // Terrible practice below, but.... meh?
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

        public async Task AutoDeleteRecentMessage()
        {
            //Cursor.Position = new Point(352, 953);
            //Cursor.Position = new Point(388, 898);

            LeftMouseClick(352, 953);
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
                            if (checkBox3.Checked != true)
                            {
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
                            if (checkBox3.Checked != true)
                            {
                                SendKeys.Send("{ENTER}");
                            }
                        }
                    }
                    else
                    {
                        char[] characters = typer.Text.ToCharArray();
                        foreach (char character in characters)
                        {
                            SendKeys.Send("{" + character.ToString() + "}");
                        }
                        if (checkBox3.Checked != true)
                        {
                            SendKeys.Send("{ENTER}");
                        }
                    }
                    StringToInt sti = new StringToInt();
                    int? time = sti.ConvertStringToInt(delaytime.Text);
                    //MessageBox.Show(time.Value.ToString()); Debug
                    await Task.Delay(time.Value * 1000);
                    await StartLoop();
                }   
            }
            catch(Exception ex)
            {
                var ste = new StackTrace(ex, true);
                var frame = ste.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show("Error: " + ex.Message+" Line Number: "+line.ToString());
            }
        }

        // Since auto delete isn't a feature yet, this isn't functional, shows a message box when clicked.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AutoDeleteMessages = checkBox1.Checked;
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
                // placeholder messagebox
                MessageBox.Show("This used to show something, not anymore.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            /*
            try
            {
                int[] CursorPos = { System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y };
                //MessageBox.Show(CursorPos[1].ToString(), ", " + CursorPos[2].ToString());
                //MessageBox.Show(System.Windows.Forms.Cursor.Position.X + ", " + System.Windows.Forms.Cursor.Position.Y);

                Cursor.Position = new Point(352, 953);
                
                Cursor.Position = new Point(388, 898);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
            */

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
