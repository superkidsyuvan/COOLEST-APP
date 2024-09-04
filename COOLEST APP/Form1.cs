using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;



namespace COOLEST_APP
{
    public partial class Form1 : Form
    {
        bool debounce = false;
        bool debounce2 = false;
        public string path = "Please Select a Path.";
        public string gameup = "1";
        public string gamelin = "https://download1584.mediafire.com/ij1u51pnsm8gsSbyvQDj_QUKeEnIbGjEVDXdvkdMJh8We85-tgdMe7jzWrK2R8t406m27nBfpUrRfnkmZBdyhFlIBpw8kOq0OP1R05Wy8vsebWYyQt33TvUHuQAlxzh-sl-mPGw8foF1cuQo7EqYnXW3HUF-A68z4NAN9Q14VDQ/l4mzyae52w6x8ni/Build.zip";
        public string EIMup = "1";
        public string EIMlin = "https://";
        public string install = "false";
        private readonly Stopwatch stopwatch = new Stopwatch();
        private AbortableBackgroundWorker unzipWorker;
        private AbortableBackgroundWorker pngWorker;
        private long timestamp = 1;
        private long bytesPreDwn = 1;
        string strpath = "";
        AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            Text = "Electronic Mayhem Launcher";
        }

        public class Jspn
        {
            public string Path { get; set; }
            public string GameUp { get; set; }
            public string GameLin { get; set; }
            public string EIMUp { get; set; }
            public string EIMLin { get; set; }
            public string Install { get; set; }
        }

        public class AbortableBackgroundWorker : BackgroundWorker
        {

            private Thread workerThread;

            protected override void OnDoWork(DoWorkEventArgs e)
            {
                workerThread = Thread.CurrentThread;
                try
                {
                    base.OnDoWork(e);
                }
                catch (ThreadAbortException)
                {
                    e.Cancel = true; //We must set Cancel property to true!
                    Thread.ResetAbort(); //Prevents ThreadAbortException propagation
                }
            }

            public void Abort()
            {
                if (workerThread != null)
                {
                    workerThread.Abort();
                    workerThread = null;
                }
            }
        }

        public static class JsonFileReader
        {
            public static Jspn Read<Jspn>(string filePath)
            {
                string text = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Jspn>(text);
            }
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
                System.Windows.Forms.Button disadv = new System.Windows.Forms.Button() { Text = "No", Left = 250, Width = 100, Top = 70, DialogResult = DialogResult.Yes };
                confirmation.Click += (sender, e) => { prompt.Close(); };
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
                    pictureBox.Image = (Image)MyImage;
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
                    pictureBox.Image = (Image)MyImage;
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
                    pictureBox.Image = (Image)MyImage;
                    pictureBox.Left = 30;
                    pictureBox.Top = 5;
                    confirmation.Text = "Yes";
                    disadv.DialogResult = DialogResult.No;
                    prompt.Controls.Add(pictureBox);
                    prompt.Controls.Add(disadv);
                }

                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                //prompt.ShowDialog();

                //prompt.AcceptButton = confirmation;

                if (prompt.DialogResult == DialogResult.Yes)
                {
                    // result = "y";
                }

                return prompt.ShowDialog() == DialogResult.Yes ? textBox.Text : "";
            }
        }

        private void disadv_Click(object sender, EventArgs e)
        {
            textBox.Text = "new";
        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Path.GetTempPath() + @"EMI\path.json"))
            {
                progressBar1.Width = 743;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                label1.Visible = true;
                label1.Text = "Checking Game..";
                Jspn item = JsonFileReader.Read<Jspn>(System.IO.Path.GetTempPath() + @"EMI\path.json");
                if (Directory.Exists(item.Path))
                {
                    path = item.Path;
                    label2.Text = item.Path;
                    if (item.Install == "true")
                    {
                        dwnbtn.Text = "Launch";
                        clrdel.Visible = true;
                    }
                    gameup = item.GameUp;
                    gamelin = item.GameLin;
                    EIMup = item.EIMUp;
                    EIMlin = item.EIMLin;
                    install = item.Install;
                }
                progressBar1.Value = 1;
                if(File.Exists(System.IO.Path.GetTempPath() + @"EMI\dwn_path.json"))
                {
                    File.Delete(System.IO.Path.GetTempPath() + @"EMI\dwn_path.json");
                }
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += dwnup_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                        new System.Uri("https://drive.google.com/uc?export=download&id=107NH2glzffbkAvdZq_QjUMSiCcY6SP9o"),
                        System.IO.Path.GetTempPath() + @"EMI\dwn_path.json"
                    );
                }

            }
            else
            {
                dwnbtn.Visible = true;
                menubtn.Visible = true;
            }
            Console.WriteLine("p" + (System.IO.Path.GetTempPath() + @"EMI\path.json"));
        }

        private void InitializeBackgroundWorker()
        {
            unzipWorker = new AbortableBackgroundWorker();
            pngWorker = new AbortableBackgroundWorker();
            unzipWorker.WorkerSupportsCancellation = true;
            pngWorker.WorkerSupportsCancellation = true;
            unzipWorker.WorkerReportsProgress = true;
            pngWorker.WorkerReportsProgress = true;
            unzipWorker.DoWork += UnzipWorker_DoWork;
            pngWorker.DoWork += pngWorker_DoWork;
            unzipWorker.ProgressChanged += UnzipWorker_ProgressChanged;
            pngWorker.ProgressChanged += pngWorker_ProgressChanged;
            unzipWorker.RunWorkerCompleted += UnzipWorker_RunWorkerCompleted;
            pngWorker.RunWorkerCompleted += pngWorker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (Directory.Exists(strpath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(strpath);
                    var files = dirInfo.GetFiles();
                    var dirs = dirInfo.GetDirectories();
                    //I assume your code is inside a Form, else you need a control to do this invocation;
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 0;
                        progressBar1.Maximum = files.Length + dirs.Length;
                        progressBar1.Step = 1;
                    }));

                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                        this.BeginInvoke(new Action(() => progressBar1.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;

                    }

                    foreach (DirectoryInfo dir in dirs)
                    {
                        dir.Delete(true);
                        this.BeginInvoke(new Action(() => progressBar1.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;
                    }
                    dirInfo.Delete();

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Maximum = 100;
                        progressBar1.Visible = false;
                        button3.Visible = false;
                        label1.Visible = false;
                        label3.Visible = false;
                        label4.Visible = false;
                        clrdel.Visible = false;
                        install = "false";
                        SaveJson(System.IO.Path.GetTempPath() + @"EMI\path.json");
                    }));


                }
            }, null);
        }

        private void UnzipWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string zipFilePath = path + "\\Build.zip";
            string extractPath = path + "\\Build";

            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);
            }catch(Exception ex)
            {
                DialogResult boxOfMessage = MessageBox.Show("Error Occured: " + ex.ToString(), "Oh Noes!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);

                if(boxOfMessage == DialogResult.Retry)
                {
                    startUnzip();
                }else if (boxOfMessage == DialogResult.Ignore)
                {
                    unzipWorker.CancelAsync();
                    startUnzip();
                }
            }
        }

        private void pngWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var file in
                    Directory.GetFiles(path + @"\Build\em_steam-win32-x64\resources\app\src_files\images\bin", "*.png", SearchOption.AllDirectories))
            {
                /* if life was simple progressBar1.Maximum = file.Length;
                progressBar1.Value = progressBar1.Value + 1;*/
                Console.WriteLine(file);
                if (!IsValidPNG(file))
                {
                    //deal with the error
                    MessageBox.Show("there is corruption (you must uninstall and reinstall yourself)", "problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pngWorker.CancelAsync();
                    break;
                }
            }
        }

        private void UnzipWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the ProgressBar value based on progress
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Unzipping Game Resources " + e.ProgressPercentage + "%";
        }

        private void pngWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the ProgressBar value based on progress
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Checking Integrity of Resources... (" + e.ProgressPercentage + "%)";
        }

        private void SaveJson(string dirPath)
        {
            if (!(Directory.Exists(System.IO.Path.GetTempPath() + @"EMI\")))
            {
                Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"EMI\");
            }
            if (Directory.Exists(System.IO.Path.GetTempPath() + @"EMI\path.json"))
            {
                File.Delete(System.IO.Path.GetTempPath() + @"EMI\path.json");
            }
            var jppn = new Jspn
            {
                Path = path,
                GameUp = gameup,
                GameLin = gamelin,
                EIMUp = EIMup,
                EIMLin = EIMlin,
                Install = install
            };

            string json = JsonSerializer.Serialize(jppn);
            System.IO.File.WriteAllText(dirPath, json);
        }

        private void UnzipWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            install = "true";
            SaveJson(System.IO.Path.GetTempPath() + @"EMI\path.json");
            SaveJson(path + @"\Build\version.json");
            dwnbtn.Text = "Launch";
            clrdel.Visible = true;
            progressBar1.Maximum = 100;
            progressBar1.Visible = false;
            button3.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            clrdel.Visible = false;
            // Unzipping completed
            //MessageBox.Show("Unzipping completed!");
        }

        private void pngWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Finished Task (no corruption!!!!)", "Completed Task 00fEI2u", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clrdel.Visible = true;
            progressBar1.Maximum = 100;
            progressBar1.Visible = false;
            button3.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            clrdel.Visible = true;
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
                if(Directory.Exists(path + @"\Build\"))
                {
                    if(!(File.Exists(path + @"\Build\version.json")))
                    {
                        SaveJson(path + @"\Build\version.json");
                    }
                    Jspn dwnjson = JsonFileReader.Read<Jspn>(System.IO.Path.GetTempPath() + @"EMI\dwn_path.json");
                    Jspn vjson = JsonFileReader.Read<Jspn>(path + @"\Build\version.json");
                    gameup = vjson.GameUp;
                    install = "true";
                    clrdel.Visible=true;
                    if (Int32.Parse(vjson.GameUp) < Int32.Parse(dwnjson.GameUp))
                    {
                        dwnbtn.Text = "Update Game";
                    }
                    else
                    {
                        dwnbtn.Text = "Launch";
                    }
                }
                else
                {
                    clrdel.Visible = false;
                    dwnbtn.Text = "Install";
                    install = "false";
                }
                SaveJson(System.IO.Path.GetTempPath() + @"EMI\path.json");
            }
        }

        private void dwnbtn_Click(object sender, EventArgs e)
        {

            //string asd = Prompt.ShowDialog("Default Previous Path Found. Should The Program use it for the Function?", "Information??", false);
            if (dwnbtn.Text == "Install" || dwnbtn.Text == "Update Game")
            {
                try
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
                            if (File.Exists(path + @"\Build.zip"))
                            {
                                File.Delete(path + @"\Build.zip");
                            }
                            if (Directory.Exists(path + @"\Build\"))
                            {
                                Directory.Delete(path + @"\Build\");
                            }
                            stopwatch.Restart();
                            using (WebClient wc = new WebClient())
                            {
                                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                                wc.DownloadFileAsync(
                                    new System.Uri(gamelin),
                                    path + "\\Build.zip"
                                );
                            }
                        }
                        if (Directory.Exists(path) is false)
                        {
                            string promptValue = Prompt.ShowDialog("Please Select a Path to Install Game via the '☰' button (Unable to Find Current Path)", "Error!!!!", false);
                        }
                    }
                    else
                    {
                        string promptValue = Prompt.ShowDialog("Please Select a Path to Install Game via the '☰' button", "Error!!!!", false);
                        //throw new CustomException("Please Select a Path to Install Game via the '☰' button");
                    }
                }
                catch (Exception ex)
                {
                    string promptValue = Prompt.ShowDialog("Unexpected Error Encountered: " + (ex.ToString()), "Error!!!!", false);
                }
            }
            if (dwnbtn.Text == "Launch")
            {
                try
                {
                    Process em = Process.Start(path + @"\Build\em_steam-win32-x64\em_steam.exe");
                    this.WindowState = FormWindowState.Minimized;
                    em.WaitForExit();
                    this.WindowState = FormWindowState.Normal;
                    /*Process ExternalProcess = new Process();
                    ExternalProcess.StartInfo.FileName = path + @"\Build\em_steam-win32-x64\em_steam.exe";
                    ExternalProcess.Start();
                    ExternalProcess.WaitForExit();
                    Console.WriteLine(path + @"\Build\em_steam-win32-x64\em_steam.exe");*/
                    //Process.Start(new ProcessStartInfo(path + @"\Build\em_steam-win32-x64\em_steam.exe"));
                    //Console.WriteLine(em.HasExited);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected Error Encountered: " + ex.ToString(), "HOW TIS EVEN HAPEN", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
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
            if (label3.Visible)
            {
                label3.Visible = false;
            }
            if (label4.Visible)
            {
                label4.Visible = false;
            }

            System.Threading.Thread.Sleep(1000);

            unzipWorker.RunWorkerAsync();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Downloading Game Resources " + e.ProgressPercentage.ToString() + "% (" + e.BytesReceived.ToString() + "Bytes /" + e.TotalBytesToReceive.ToString() + "Bytes )";
            label3.Text = "Download Speed "+ ((e.BytesReceived - bytesPreDwn) / (stopwatch.ElapsedMilliseconds - (timestamp - 1))) + " Bytes /ms";

            //long totalEstimatedMilliseconds = stopwatch.ElapsedMilliseconds * e.TotalBytesToReceive / e.TotalBytesToReceive;
            long totalEstimatedMilliseconds;
            try
            {
                /*Console.WriteLine(e.BytesReceived);
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                Console.WriteLine(timestamp);
                Console.WriteLine(bytesPreDwn);*/
                totalEstimatedMilliseconds = (e.TotalBytesToReceive - e.BytesReceived) / ((e.BytesReceived - bytesPreDwn) / (stopwatch.ElapsedMilliseconds - (timestamp - 1)));
            }
            catch (Exception ex) {
                totalEstimatedMilliseconds = 99999999999999;
                Console.WriteLine(ex.ToString());
            }
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

            bytesPreDwn = e.BytesReceived;
            timestamp = stopwatch.ElapsedMilliseconds;
        }

        void dwnup_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = System.Convert.ToInt32(e.ProgressPercentage/2) + 1;
            label1.Text = "Checking If Game Resources Are Updated To Current Standards " + (System.Convert.ToInt32(e.ProgressPercentage / 2) + 1).ToString() + "% (" + e.BytesReceived.ToString() + "Bytes /" + e.TotalBytesToReceive.ToString() + "Bytes )";
            if (e.ProgressPercentage == 100)
            {
                if (debounce2 == false)
                {
                    Jspn item2 = JsonFileReader.Read<Jspn>(System.IO.Path.GetTempPath() + @"EMI\dwn_path.json");
                    if (gameup != item2.GameUp && install == "true")
                    {
                        dwnbtn.Text = "Update Game";

                    }
                    if (gameup != item2.GameUp || gamelin != item2.GameLin || EIMup != item2.EIMUp || EIMlin != item2.EIMLin)
                    {
                        gameup = item2.GameUp;
                        gamelin = item2.GameLin;
                        EIMup = item2.EIMUp ;
                        EIMlin = item2.EIMLin;
                    }
                    progressBar1.Visible = false;
                    progressBar1.Width = 521;
                    progressBar1.Value = 0;
                    label1.Visible = false;
                    dwnbtn.Visible = true;
                    menubtn.Visible = true;
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

        private void clrdel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete game files? THIS IS AN IRREVERSIBLE DECISION.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Get the file path to delete
            string filePath = path + @"\Build.zip"; // Implement logic to get file path
            string folderPath = path + @"\Build\";

            try
            {
                if (dialogResult == DialogResult.Yes)
                {
                    progressBar1.Visible = true;
                    button3.Visible = true;
                    label1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    // Check if file exists
                    if (File.Exists(filePath))
                    {
                        // Update UI to show deletion started
                        
                        label1.Text = "Deleting Game Zip (1/2)";

                        File.Delete(filePath);
                    }
                    if(Directory.Exists(folderPath))
                    {
                        label1.Text = "Deleting Game Folder (2/2)";
                        strpath = folderPath;
                        worker.RunWorkerAsync();
                    }
                    else
                    {
                        progressBar1.Maximum = 100;
                        progressBar1.Visible = false;
                        button3.Visible = false;
                        label1.Visible = false;
                        label3.Visible = false;
                        label4.Visible = false;
                        clrdel.Visible = false;
                        install = "false";
                        SaveJson(System.IO.Path.GetTempPath() + @"EMI\path.json");
                    }

                    // delete folder(s)...
                }
            }
            catch (Exception ex)
            {
                DialogResult opies = MessageBox.Show("Unexpected Error Encountered: " + ex.ToString(), "HOW TIS EVEN HAPEN", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if(opies == DialogResult.Retry)
                {
                    clrdel_Click(clrdel,EventArgs.Empty);
                }
            }
        }

        private void RemoveDirectories(string strpath)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (Directory.Exists(strpath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(strpath);
                    var files = dirInfo.GetFiles();
                    var dirs = dirInfo.GetDirectories();
                    //I assume your code is inside a Form, else you need a control to do this invocation;
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 0;
                        progressBar1.Maximum = files.Length + dirs.Length;
                        progressBar1.Step = 1;
                    }));

                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                        this.BeginInvoke(new Action(() => progressBar1.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;

                    }

                    foreach (DirectoryInfo dir in dirs)
                    {
                        dir.Delete(true);
                        this.BeginInvoke(new Action(() => progressBar1.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;
                    }
                    dirInfo.Delete();

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Maximum = 100;
                        progressBar1.Visible = false;
                        button3.Visible = false;
                        label1.Visible = false;
                        label3.Visible = false;
                        label4.Visible = false;
                        clrdel.Visible = false;
                    }));

                    
                }
            }, null);
        }

        private void chkres_Click(object sender, EventArgs e)
        {
            //ui

            progressBar1.Value = 0;
            clrdel.Visible = false;
            progressBar1.Maximum = 100;
            progressBar1.Visible = true;
            button3.Visible = true;
            label1.Visible = true;
            clrdel.Visible = true;
            label1.Text = "Checking Integrity of Resources...";
            if (label3.Visible)
            {
                label3.Visible = false;
            }
            if (label4.Visible)
            {
                label4.Visible = false;
            }

            System.Threading.Thread.Sleep(1000);

            pngWorker.RunWorkerAsync();

            // check pngs (pretend that everything is png)
            {
                //start pngWorker
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static bool IsValidPNG(string filename)
        {
            try
            {
                using (var bmp = new Bitmap(filename)) { }
                return true;
            } catch(Exception e)
            {
                return false;
            }
        }

    }
}

