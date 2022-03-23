using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string targetDirectory = "";

        private void button1_Click(object sender, EventArgs e)
        {          
            DialogResult bd = folderBrowserDialog1.ShowDialog();
            if (bd == DialogResult.OK)
            {
                targetDirectory = folderBrowserDialog1.SelectedPath;
                new Thread(delegate()
                {
                    int count = 0;
                    ImageList imageList = new ImageList();
                    imageList.ImageSize = new Size(200, 200);
                    foreach (string file in Directory.GetFiles(targetDirectory))
                    {
                        try
                        {
                            Image image = Image.FromFile(file);
                            imageList.Images.Add(image);
                            count++;
                        }
                        catch
                        { }
                    }

                    Invoke((Action)(() =>
                    {
                        listView1.SmallImageList = imageList;
                        for (int i = 0; i < count; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.ImageIndex = i;
                            listView1.Items.Add(lvi);
                        }
                    }));
                }).Start();
            }  
            
        }        
    }
}
