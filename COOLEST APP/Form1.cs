using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace COOLEST_APP
{
    public partial class Form1 : Form
    {
        bool debounce = false;
        string path = "Please Select a Path.";
        private readonly Stopwatch stopwatch = new Stopwatch();
        private BackgroundWorker unzipWorker;
        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }


        public static class Prompt
        {
            public static string ShowDialog(string text, string caption, bool tl)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 100, Top = 20, Width = 400, Text = text };
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 400 };
                System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                if (tl == true)
                {
                    prompt.Controls.Add(textBox);
                }
                if (caption == "Error!!!!")
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap MyImage = new Bitmap("C:\\Windows\\System32\\SecurityAndMaintenance_Error.png");
                    pictureBox.ClientSize = new Size(50, 50);
                    pictureBox.Image = (Image) MyImage ;
                    pictureBox.Left = 30;
                    pictureBox.Top = 5;
                    prompt.Controls.Add(pictureBox);
                }
                if (caption == "Warning..")
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap MyImage = new Bitmap("C:\\Windows\\System32\\SecurityAndMaintenance_Alert.png");
                    pictureBox.ClientSize = new Size(50, 50);
                    pictureBox.Image = (Image) MyImage;
                    pictureBox.Left = 30;
                    pictureBox.Top = 5;
                    prompt.Controls.Add(pictureBox);
                }
                if (caption == "Information??")
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap MyImage = new Bitmap("C:\\Windows\\System32\\SecurityAndMaintenance.png");
                    pictureBox.ClientSize = new Size(50, 50);
                    pictureBox.Image = (Image) MyImage;
                    pictureBox.Left = 30;
                    pictureBox.Top = 5;
                    prompt.Controls.Add(pictureBox);
                }
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void InitializeBackgroundWorker()
        {
            unzipWorker = new BackgroundWorker();
            unzipWorker.WorkerReportsProgress = true;
            unzipWorker.DoWork += UnzipWorker_DoWork;
            unzipWorker.ProgressChanged += UnzipWorker_ProgressChanged;
            unzipWorker.RunWorkerCompleted += UnzipWorker_RunWorkerCompleted;
        }

        private void UnzipWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string zipFilePath = path + "\\Build.zip";
            string extractPath = path + "\\Build";

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }

        private void UnzipWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the ProgressBar value based on progress
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Unzipping Game Resources "+ e.ProgressPercentage + "%";
        }

        private void UnzipWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Unzipping completed
            //MessageBox.Show("Unzipping completed!");
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
            if (path == "")
            {
                path = "Please Select a Path.";
                label2.Text = "Path Selection was Canceled.";
            }
            else
            {
                if (Directory.Exists(path + @"\EMI\path.json" ))
                {

                }
                else
                {
                    Console.WriteLine(path);
                    Console.WriteLine(path + @"\EMI\path.json");
                    Console.WriteLine(System.IO.Path.GetTempPath() + @"\EMI\path.json");
                }
            }
        }

        private void dwnbtn_Click(object sender, EventArgs e)
        {
            
            if (path != "Please Select a Path.")
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("Started, " + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
                    progressBar1.Visible = true;
                    button3.Visible = true;
                    label1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    stopwatch.Restart();
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                        wc.DownloadFileAsync(
                            new System.Uri("https://download1584.mediafire.com/ij1u51pnsm8gsSbyvQDj_QUKeEnIbGjEVDXdvkdMJh8We85-tgdMe7jzWrK2R8t406m27nBfpUrRfnkmZBdyhFlIBpw8kOq0OP1R05Wy8vsebWYyQt33TvUHuQAlxzh-sl-mPGw8foF1cuQo7EqYnXW3HUF-A68z4NAN9Q14VDQ/l4mzyae52w6x8ni/Build.zip"),
                            path + "\\Build.zip"
                        );
                    }
                }
            }
            else
            {
                string promptValue = Prompt.ShowDialog("Please Select a Path to Install Game via the '☰' button", "Error!!!!", false);
                //throw new CustomException("Please Select a Path to Install Game via the '☰' button");
            }
        }

        private void menubtn_Click(object sender, EventArgs e)
        {
            if (panel.Visible == false)
            {
                panel.Visible = true;
                return;
            }

            if (panel.Visible == true)
            {
                panel.Visible = false;
                return;
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            
        }
        
        private void panel_Exit(object sender, EventArgs e)
        {
           
        }

        private void startUnzip()
        {

            progressBar1.Value = 0;
            label1.Text = "Initializing Unzip Process...";
            label3.Visible = false;
            label4.Visible = false;

            System.Threading.Thread.Sleep(1000);

            unzipWorker.RunWorkerAsync();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Downloading Game Resources " + e.ProgressPercentage.ToString() + "% (" + e.BytesReceived.ToString() + "Bytes /" + e.TotalBytesToReceive.ToString() + "Bytes )";
            
            long totalEstimatedMilliseconds = stopwatch.ElapsedMilliseconds * e.TotalBytesToReceive / e.TotalBytesToReceive;
            TimeSpan remainingTime = TimeSpan.FromMilliseconds(totalEstimatedMilliseconds);

            // Convert remaining time to hours, minutes, and seconds
            string remainingTimeString = $"{remainingTime.Hours}h {remainingTime.Minutes}m {remainingTime.Seconds}s";
            label4.Text = $"Estimated Remaining Time: {remainingTimeString}";

            if (e.ProgressPercentage == 100)
            {
                if (debounce == false)
                {
                    debounce = true;
                    Console.WriteLine("done, " + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());

                    startUnzip();

                    //System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);


                    //ZipFile.ExtractToDirectory(path + "\\Build.zip", path + "\\Build");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
