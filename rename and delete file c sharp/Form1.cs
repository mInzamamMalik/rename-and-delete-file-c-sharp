using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace rename_and_delete_file_c_sharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            refresh();
        }

        public void refresh()
        {
            DriveInfo[] allDrivesList = DriveInfo.GetDrives();

            comboBox1.ResetText();

            foreach (var item in allDrivesList)
            {
                //MessageBox.Show(items.ToString());
                comboBox1.Items.Add(item.ToString());
            }
        }
        



        private void getFilesAllOfThisDrive(object sender, EventArgs e)
        {
            try
            {

                // Get driveinfo of a drive path
                System.IO.DriveInfo di = new System.IO.DriveInfo(comboBox1.Text);
                textBox1.Text = (di.TotalSize / 1024 / 1024).ToString() + " MB";
                textBox2.Text = (di.TotalFreeSpace / 1024 / 1024).ToString() + " MB";
                textBox3.Text = di.VolumeLabel.ToString();
                textBox4.Text = di.DriveFormat.ToString();

                // Get the root directory of a drive using driveinfo
                System.IO.DirectoryInfo dirInfo = di.RootDirectory;

                // Get the files in the directory
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");
                listBox1.Items.Clear();

                foreach (var item in fileNames)
                {
                    listBox1.Items.Add(item.ToString());
                }

            }
            catch (Exception)
            {
                MessageBox.Show("an exception occured");
                listBox1.Items.Clear();
            }            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = comboBox1.Text + listBox1.SelectedItem;
        }       

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (newFileName.Text == "")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                button2.Enabled = false;
                newFileName.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                newFileName.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Move(@textBox5.Text, @comboBox1.Text + newFileName.Text);
            MessageBox.Show("file renamed to " + newFileName.Text);
            newFileName.Text = "";
         
         }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(@textBox5.Text);
            MessageBox.Show("file is deleted");
            refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
