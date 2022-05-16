using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int numberOfFrames = 0;
        int currentFrame = 0;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (FileIsLocked())
            {
                UnlockListFile();
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            DownloadListFile();

            if (!File.Exists("alreadydrew.txt"))
            {
                foreach (var line in nameList)
                {
                    comboBox1.Items.Add(line.Split('*')[0]);
                }
            }
            else
            {
                button1.Visible = false;
                YourNameLabel.Visible = false;
                comboBox1.Visible = false;
                HelpLabel.Visible = false;
                HelpTextBox.Visible = false;
                if (CompareChecksum())
                {
                    try
                    {
                        var who = nameList.Where(x => x.StartsWith(File.ReadAllText("alreadydrew.txt") + "*")).First().Split('*');
                        label1.Text = "Akit húztál: " + who[0];
                        for (int i = 1; i < who.Length; i++)
                        {
                            if (who[i] != "HUZOTT")
                            {
                                DrewHelpLabel.Text = "Akit húztál ezt adta meg: " + who[i];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("well this is unexpected");
                    }
                }
                else
                {
                    label1.Text = "Nice try";
                    pictureBox1.Visible = true;
                    var image = new Bitmap(new MemoryStream(new WebClient().DownloadData(@"http://w3.hdsnet.hu/danxdlul/ace-attorney.gif")));

                    pictureBox1.Image = image;
                    pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);

                    FrameDimension dimension = new FrameDimension(this.pictureBox1.Image.FrameDimensionsList[0]);
                    numberOfFrames = this.pictureBox1.Image.GetFrameCount(dimension);

                }
            }
        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (currentFrame == numberOfFrames-1)
            {
                var image = new Bitmap(new MemoryStream(new WebClient().DownloadData(@"http://w3.hdsnet.hu/danxdlul/ace-attorney2.gif")));
                this.pictureBox1.Image = image;

            }
            currentFrame++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            if (FileIsLocked())
            {
                label1.Invoke((MethodInvoker)(() => label1.Text = "Jelenleg valaki más készül húzásra."));

                Task.Run(() =>
                {
                    while (FileIsLocked())
                    {
                        Thread.Sleep(1000);
                    }
                }).Wait();
                
            }
            
            label1.Text = "";
            LockListFile();
            DownloadListFile();
            if (DidAlreadyDraw(comboBox1.Text))
            {
                UnlockListFile();
                label1.Text = "Te már húztál.";
                return;
            }
            int nmb;
            var rnd = new Random();
            do
            {
                nmb = rnd.Next(0, nameList.Count);
            }
            while (comboBox1.Text == nameList[nmb] || nameList[nmb].Split('*').Any(x => x == "HUZOTT"));
            label1.Text = "Akit húztál: " + nameList[nmb].Split('*')[0];
            nameList[nmb] = nameList[nmb] + "*HUZOTT";

            var myNameIndex = nameList.FindIndex(x => x.StartsWith(comboBox1.Text));
            nameList[myNameIndex] = nameList[myNameIndex] + "*" + HelpTextBox.Text;

            UploadListFile();
            UnlockListFile();
            File.WriteAllText("alreadydrew.txt", nameList[nmb].Split('*')[0]);
            File.SetAttributes("alreadydrew.txt", FileAttributes.Hidden | FileAttributes.ReadOnly);
            UploadCheckSum();
            comboBox1.Enabled = false;
        }

        private bool DidAlreadyDraw(string name)
        {
            var myName = nameList.FirstOrDefault(x => x.Contains(name + "*"));
            if(myName == null)
            {
                return false;
            }
            var splits = myName.Split('*');
            for(int i = 1; i< splits.Length; i++)
            {
                if(splits[i] != "HUZOTT")
                return true;
            }
            return false;
        }

        private void DownloadListFile()
        {
            nameList.Clear();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePathAtBackend);

            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();


            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            do
            {
                try
                {
                    nameList.Add(reader.ReadLine());
                }catch (Exception)
                {
                    break;
                }
                
            }
            while (true);
            response.Close();
            

        }

        private void UploadCheckSum()
        {
            var user = Environment.UserName;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp:\\w3.hdsnet.hu/"+user);


            request.Credentials = new NetworkCredential(username, password);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            var cs = MD5.Create().ComputeHash(File.OpenRead("alreadydrew.txt"));

            using (Stream sw = request.GetRequestStream())
            {
                sw.Write(cs, 0, cs.Length);  //sending the content to the FTP Server
            }
        }

        private byte[] GetCheckSum()
        {
            var user = Environment.UserName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp:\\w3.hdsnet.hu/" + user);


            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();


            Stream responseStream = response.GetResponseStream();

            var ms = new MemoryStream();
            responseStream.CopyTo(ms);

            return ms.ToArray();
        }

        private void UploadListFile()
        {
            FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(filePathAtBackend);


            deleteRequest.Credentials = new NetworkCredential(username, password);
            deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            deleteRequest.GetResponse();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePathAtBackend);


            request.Credentials = new NetworkCredential(username, password);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            byte[] fileContent = nameList.SelectMany(s =>
System.Text.Encoding.UTF8.GetBytes(s + Environment.NewLine)).ToArray();

            using (Stream sw = request.GetRequestStream())
            {
                sw.Write(fileContent, 0, fileContent.Length-1);  //sending the content to the FTP Server
            }
        }
        private void LockListFile()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(lockFilePathAtBackend);


            request.Credentials = new NetworkCredential(username, password);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            byte[] fileContent = { };
            using (Stream sw = request.GetRequestStream())
            {
                sw.Write(fileContent, 0, 0);  //sending the content to the FTP Server
            }
        }
        public void UnlockListFile()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(lockFilePathAtBackend);


            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            request.GetResponse();
        }
        public bool FileIsLocked()
        {
            var request = (FtpWebRequest)WebRequest.Create(lockFilePathAtBackend);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

        private bool CompareChecksum()
        {
            try
            {
                var checksum = GetCheckSum();
                var compareWith = MD5.Create().ComputeHash(File.OpenRead("alreadydrew.txt"));
                return DoesChecksumsMatch(checksum, compareWith);
            }
            catch(Exception ex)
            {
                return false;
            }
            

            
        }

        private bool DoesChecksumsMatch(byte[] hashOne, byte[] hashTwo)
        {
            bool bEqual = false;
            if (hashOne.Length == hashTwo.Length)
            {
                int i = 0;
                while ((i < hashOne.Length) && (hashOne[i] == hashTwo[i]))
                {
                    i += 1;
                }
                if (i == hashTwo.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }

        private readonly string filePathAtBackend = @"ftp:\\w3.hdsnet.hu/list.txt";
        private readonly string lockFilePathAtBackend = @"ftp:\\w3.hdsnet.hu/lockfile";
        private readonly string username = "temp";
        private readonly string password = "temp";
        private List<string> nameList = new List<string>();
    }
}
