using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPerfectScreen
{
    public partial class Form1 : Form
    {
        //enum
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001           
        }

        // method
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        static void PreventSleep()
        {
            // Prevent Idle-to-Sleep (monitor not affected) (see note above)
            SetThreadExecutionState(
            EXECUTION_STATE.ES_CONTINUOUS |
            EXECUTION_STATE.ES_DISPLAY_REQUIRED |
            EXECUTION_STATE.ES_SYSTEM_REQUIRED
            );
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;

            Thread t = new Thread(() => {

                Random rn = new Random();
                while (true)
                {
                    PreventSleep();
                    Thread.Sleep(10000);
                }                
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form1 = new Form();

            form1.WindowState = FormWindowState.Minimized;
            form1.FormBorderStyle = FormBorderStyle.None;
            form1.WindowState = FormWindowState.Maximized;
            form1.StartPosition = FormStartPosition.Manual;

            PictureBox pb = new PictureBox();

            pb.Image = Image.FromFile(@"1.PNG");
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Dock = DockStyle.Fill;
            pb.Click += new EventHandler(closForms);

            form1.Controls.Add(pb);

            form1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {           
            Form form2 = new Form();

            Rectangle bounds = Screen.AllScreens[1].Bounds;
            form2.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            form2.WindowState = FormWindowState.Minimized;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.WindowState = FormWindowState.Maximized;
            form2.StartPosition = FormStartPosition.Manual;

            PictureBox pb = new PictureBox();

            pb.Image = Image.FromFile(@"2.PNG");
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Dock = DockStyle.Fill;
            pb.Click += new EventHandler(closForms);

            pb.Left = Screen.AllScreens[0].Bounds.Width;

            form2.Controls.Add(pb);

            form2.Show();
        }

        private void closForms(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }

      
    }
}
