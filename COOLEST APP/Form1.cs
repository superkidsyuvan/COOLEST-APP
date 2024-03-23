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



namespace COOLEST_APP
{
    public partial class Form1 : Form
    {
        bool debounce = false;
        string path = "new";
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox.Text = "new";
        }

        private void textBox_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = Dialog.ShowDialog(this);
            label2.Text = Dialog.SelectedPath;
            path = Dialog.SelectedPath;


        }

        private void dwnbtn_Click(object sender, EventArgs e)
        {
            if(label2.Text!= "new") {
                Console.WriteLine("started, " + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                        new System.Uri("https://download1584.mediafire.com/wqub0469ph3gfLdFV4ExULnVhqKU4EWNVsM5b90IfbggAMe2W_th65TPSgJJQKiTUUSONetZzJWYPPIvDuznATEgYeX1xmcrqa105Y9AMOfB6knBxV703bRlES8rHF6XOVn9Q1RkWXF3s-ADEAHe6a-Tf6cR34FuhhnFil6JOM4/l4mzyae52w6x8ni/Build.zip"),
                        path+ "\\Build.zip"
                    );
                }
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                if (debounce == false)
                {
                    debounce = true;
                    Console.WriteLine("done, " + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());

                    string zipPath = path + "\\Build.zip";
                    string extractPath = path +"\\Build";

                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);


                    //ZipFile.ExtractToDirectory(path + "\\Build.zip", path + "\\Build");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
