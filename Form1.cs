using BlockyAPI.BLOCKY;
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

namespace BlockyAPI
{
    public partial class Form1 : Form
    {
        BlockyInterface interfacer = new BlockyInterface();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var re = interfacer.Response();
            if(re==null)
            {
                String res = "";
                interfacer.tempRep.ForEach((ex) => res+=ex.errorMessage+" "+ex.sender.ConvertToCPlusPlus+"\n");
                MessageBox.Show(res);
            }
            else
            {
                MessageBox.Show(re);
            }
            this.pictureBox1.Image = interfacer.GetBitmapOfProgram();
            
        }

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        Block selectedBlock;
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
                selectedBlock = interfacer.GetSelectedBlock(e.Location);
                this.pictureBox1.Image = interfacer.GetBitmapOfProgram();
            try
            {
                //MessageBox.Show(selectedBlock.ToString());
            }
            catch { }
                timer1.Start();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            selectedBlock = null;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (selectedBlock != null)
            {
                selectedBlock.position = pictureBox1.PointToClient(MousePosition);
                try
                {
                    selectedBlock.fatherBlock.instructions.Remove(selectedBlock);
                }
                catch { }
                try
                {
                    selectedBlock.fatherBlock.parameters.Remove(selectedBlock);
                }
                catch { }
                selectedBlock.fatherBlock = null;
                interfacer.space.FitBlockByPoint(pictureBox1.PointToClient(MousePosition), selectedBlock);
                if(selectedBlock.fatherBlock == null)
                {
                    selectedBlock.fatherBlock = interfacer.space;
                    interfacer.space.instructions.Add(selectedBlock);
                }
                this.pictureBox1.Image = interfacer.GetBitmapOfProgram();
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            selectedBlock = null;
        }
    }
}
