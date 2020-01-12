using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY.Operation_Blocks
{
    class BlockOperation : Block
    {
        #region Constructor
        public BlockOperation(BlockOperationType type)
        {
            this.scheme = (BlockScheme)hashOperations[type];
            this.parameters = new List<Block>();
            for (int i = 0; i < scheme.numOfParams; i++)
                this.parameters.Add(null);
        }
        #endregion

        #region Error checking
        public override List<BlockException> CheckForErrors => BlockyHelpers.CheckForErrorsAndNulls(this.parameters);
        #endregion

        #region Convert to C++
        public override string ConvertToCPlusPlus => this.parameters[0].ConvertToCPlusPlus + " " + this.scheme.text + " " + this.parameters[1].ConvertToCPlusPlus;
        #endregion
        public override int Width
        {
            get
            {
                Graphics g = Graphics.FromImage(new Bitmap(5, 5));
                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                int width = (int)sizeOfString.Width+1;
                this.parameters.ForEach((par) => width += par.Width);
                return width;
            }
        }

        public override int Height
        {
            get
            {
                //normal height of a block not affected by others is 10 but when height of block gets over 10
                //it changes to that item thus the max between 10 and the parameters height is the blocks height
                Graphics g = Graphics.FromImage(new Bitmap(5, 5));
                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                int maxim = (int)sizeOfString.Height+1;
                this.parameters.ForEach((par) => maxim = Math.Max(maxim, par.Height));
                return maxim;
            }
        }

        public override Bitmap DrawToBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bmp);
                Brush textBrush = new SolidBrush(Color.Black);
                Brush blockBrush = new SolidBrush(Color.Purple);
                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));

                if (scheme.numOfParams == 1)
                {
                    g.FillRectangle(blockBrush, new Rectangle(0, 0, (int)sizeOfString.Width+1, (int)sizeOfString.Height + 1));
                    g.DrawString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)), textBrush, new Rectangle(new Point(0,Height/2-this.textHeightMulti/2), new Size((int)sizeOfString.Width+1, (int)sizeOfString.Height+1)));
                    g.DrawImage(this.parameters[0].DrawToBitmap, new Point(0 + scheme.text.Length * this.textWidthMulti, 5));
                }
                else
                {
                    g.FillRectangle(blockBrush, new Rectangle(this.parameters[0].Width, 0, (int)sizeOfString.Width+1, Height));
                    g.FillRectangle(blockBrush, new Rectangle(0, 0, Width, 5));
                    g.FillRectangle(blockBrush, new Rectangle(0, Height-5, Width, 5));

                    g.DrawString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)), textBrush, new Rectangle(new Point(0 + this.parameters[0].Width, Height / 2 - this.textHeightMulti/2), new Size((int)sizeOfString.Width + 1, (int)sizeOfString.Height + 1)));
                    g.DrawImage(this.parameters[0].DrawToBitmap, new Point(0,0));
                    g.DrawImage(this.parameters[1].DrawToBitmap, new Point(this.parameters[0].Width + (int)sizeOfString.Width+1, 0));
                }
                return bmp;
            }
        }

        public override void FitBlockByPoint(Point position, Block block)
        {
            if (BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X+0, this.position.Y+0))<=5.0)
            {
                this.ChangeParameter(block, 0);
                block.fatherBlock = this;
            }
            else if(BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X +Width +this.parameters[1].Width, this.position.Y + 0)) <= 5.0)
            {
                this.ChangeParameter(block, 1);
                block.fatherBlock = this;

            }
            else
            {
                this.parameters.ForEach((par) => par.FitBlockByPoint(position, block));
            }
        }

        public override Block GetSelectedBlock(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
            if (BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X+this.parameters[0].Width, this.position.Y, (int)sizeOfString.Width + 1, Height)))
                return this;
            else
            {
                Block block = null;
                this.parameters.ForEach((par) => {
                    Block temp = par.GetSelectedBlock(point);
                    if (temp != null)
                        block = temp;
                });
                return block;
            }
        }
    }
}
