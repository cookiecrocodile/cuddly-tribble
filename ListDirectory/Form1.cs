using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListDirectory
{

    public partial class Form1 : Form
    {
        string drivePath;
        string currentPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DriveInfo[] di = DriveInfo.GetDrives();

            foreach (DriveInfo itemDrive in di)
            {
                comboBoxDrives.Items.Add(itemDrive.Name);
            }
        }

        private void comboBoxDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxDirectories.Items.Clear();
            listBoxFiles.Items.Clear();

            try
            {
                drivePath = comboBoxDrives.SelectedItem.ToString(); //TESTKOD

                listBoxDirectories.Items.Clear();
                //DriveInfo di = new DriveInfo(comboBoxDrives.SelectedItem.ToString());
                DriveInfo di = new DriveInfo(drivePath);
                // DirectoryInfo root = new DirectoryInfo(comboBoxDrives.SelectedItem.ToString());
                DirectoryInfo root = new DirectoryInfo(drivePath);
                DirectoryInfo[] dir = root.GetDirectories();
                FileInfo[] files = root.GetFiles();

                currentPath = drivePath;


                foreach (DirectoryInfo d in dir)
                {
                    listBoxDirectories.Items.Add(d.Name);
                }

                foreach (FileInfo fi in files)
                {
                    listBoxFiles.Items.Add(fi.Name);
                }

                MessageBox.Show("Available Free Space: " + di.AvailableFreeSpace + "\n" +
                    "Drive Format: " + di.DriveFormat + "\n" +
                    "Drive Type: " + di.DriveType + "\n" +
                    "Is Ready: " + di.IsReady.ToString() + "\n" +
                    "Name: " + di.Name + "\n" +
                    "Root Directory: " + di.RootDirectory + "\n" +
                    "Total Free Space: " + di.TotalFreeSpace + "\n" +
                    "Total Size: " + di.TotalSize + "\n" +
                    "Volume Label: " + di.VolumeLabel.ToString(), di.Name +
                    " DRIVE INFORMATION");

                ShowCurrentPath();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                    listBoxFiles.Items.Clear(); //clears the files listbox

                    DirectoryInfo root = new DirectoryInfo(currentPath);
                    DirectoryInfo[] dir = root.GetDirectories();
                    FileInfo[] files = root.GetFiles();

                if (listBoxDirectories.SelectedIndex >= 0)
                {
                    int index = listBoxDirectories.SelectedIndex;
                    currentPath = dir[index].FullName;
                    listBoxDirectories.Items.Clear();

                    root = new DirectoryInfo(currentPath);
                    dir = root.GetDirectories();
                    files = root.GetFiles();
                }

                listBoxDirectories.Items.Clear(); //To prevent duplication bug

                foreach (DirectoryInfo d in dir)
                    {
                        listBoxDirectories.Items.Add(d.Name);
                    }


                    foreach (FileInfo fi in files)
                    {
                        listBoxFiles.Items.Add(fi.Name);
                    }

                    ShowCurrentPath();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DirectoryInfo root = new DirectoryInfo(currentPath);
                FileInfo[] files = root.GetFiles();

                int index = listBoxFiles.SelectedIndex; 
                string filePath = files[index].FullName;

                listBoxFiles.Items.Clear();

                foreach (FileInfo fi in files)
                {
                    listBoxFiles.Items.Add(fi.Name);
                }

                ShowCurrentPath();


                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read); //innan merTest
                StreamReader sr = new StreamReader(fs);
                String buff = sr.ReadToEnd();
                textBoxPreview.Text = buff;
                //MessageBox.Show(buff);
                sr.Close();
                fs.Close();


            }
            catch
            {
                MessageBox.Show("Not a file.");
            }

        }

        public void ShowCurrentPath()
        {
            textBoxPath.Text = currentPath;

        }

        private void buttonDirectoryUp_Click(object sender, EventArgs e)
        {
            if (currentPath != drivePath)
            {
                currentPath = Directory.GetParent(currentPath).ToString();
                listBoxDirectories.Items.Clear();
                listBoxDirectories_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("You are on the top level");
            }
        }
    }
}

