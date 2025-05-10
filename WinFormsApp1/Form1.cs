
using System.Diagnostics;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread thread = new Thread(DoWork1);
            Thread thread1 = new Thread(DoWork2);
            Thread thread2 = new Thread(DoWork3);
            thread.Start();
            thread1.Start();
            thread2.Start();
        }
        ManualResetEvent mre = new ManualResetEvent(false);
        private void DoWork1()
        {
            while (true)
            {
                mre.WaitOne();
                Thread.Sleep(1000);
                Debug.WriteLine(111111);
            }
        }
        private void DoWork2()
        {
            while (true)
            {
                if (!mre.WaitOne())
                {
                    Thread.Sleep(1000);
                    Debug.WriteLine(22222222);
                }    
            }
        }
        private void DoWork3()
        {
            while (true)
            {
                if (!mre.WaitOne(0))
                {
                    Thread.Sleep(1000);
                    Debug.WriteLine(33333333);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mre.Set();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mre.Reset();
        }
    }
}
