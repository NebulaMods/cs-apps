using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Security.Cryptography;

namespace Scanner
{
    public partial class Dupe_Checker : Form
    {
        private object syncObj = new object();
        public Dupe_Checker()
        {
            InitializeComponent();
            textBox1.Text = $@"{Environment.CurrentDirectory}\";
        }
        int TaskID;
        private void button1_Click(object sender, EventArgs e)
        {
            TaskID = Task.Run(() =>
            {
                List<string> newlist = new List<string>();
                foreach (string ip in File.ReadAllLines($"{textBox1.Text}.txt"))
                {
                    lock(syncObj) { }
                    if (!newlist.Contains(ip))
                        newlist.Add(ip);
                }
                File.WriteAllLines($"{textBox1.Text}new.txt", newlist);
                syncObj = new object();
                ActiveForm.Invoke(new MethodInvoker(delegate { MessageBox.Show("finished writting all files!"); }));
            }).Id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TaskID = Task.Run(() =>
            {
                if (!textBox1.Text.EndsWith(@"\"))
                    textBox1.Text += @"\";
                foreach (string txtfile in Directory.GetFiles(textBox1.Text))
                {
                    List<string> newlist = new List<string>();
                    if (Path.GetExtension(txtfile) == ".txt")
                    {
                        foreach (string host in File.ReadAllLines(txtfile))
                        {
                            lock (syncObj) { }
                            if (!newlist.Contains(host))
                                newlist.Add(host);
                        }
                        File.WriteAllLines($"{txtfile.Remove(txtfile.Length - 4, 4)}new.txt", newlist);
                    }
                }
                syncObj = new object();
                ActiveForm.Invoke(new MethodInvoker(delegate { MessageBox.Show("finished writting all files!"); }));
            }).Id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (syncObj.ToString() == "System.Object")
            {
                MessageBox.Show("You have no running task, please try again.", "No Running Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (button3.Text == "Stop Scan")
            {
                button3.Text = "Resume Scan";
                Monitor.Enter(syncObj);
            }
            else
            {
                button3.Text = "Stop Scan";
                Monitor.Exit(syncObj);
            }
        }
    }
}
