using System;
using System.Collections.Generic;
using System.Drawing;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    public class BlockVariable : Block
    {
        #region Variables
        public String name;
        public bool declared;
        public String value;
        #endregion

        #region Check for Errors
        //No errors for variables
        public override List<BlockException> CheckForErrors => new List<BlockException>();
        #endregion

        #region Get return type of variable
        //Returns array for convinience
        public override List<BlockReturnType> GetReturnTypes()
        {
            List<BlockReturnType> returned = new List<BlockReturnType>();
            if(this.GetType()==typeof(BlockInteger))
            {
                returned.Add(BlockReturnType.Integer);
            }
            else if(this.GetType()==typeof(BlockReal))
            {
                returned.Add(BlockReturnType.Real);
            }
            else if(this.GetType()==typeof(BlockString))
            {
                returned.Add(BlockReturnType.String);
            }
            else
            {
                returned.Add(BlockReturnType.Array);
            }
            return returned;
        }
        #endregion

        #region Convert to C++
        //Converts to C++ => only returns name because there is not anything else to do.
        public override string ConvertToCPlusPlus => name;
        #endregion


        public override int Width {
            get
            {
                string toWriteString = this.declared ? this.name.ToString(): this.value.ToString();
                Graphics g = Graphics.FromImage(new Bitmap(10, 10));
                var sizeOfString = g.MeasureString(toWriteString, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                return (int)sizeOfString.Width+1;
            }
        }

        public override int Height
        {
            get
            {
                string toWriteString = this.declared ? this.name.ToString() : this.value.ToString();
                Graphics g = Graphics.FromImage(new Bitmap(10, 10));
                var sizeOfString = g.MeasureString(toWriteString, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                return (int)sizeOfString.Height + 1;
            }
        }

        public override Bitmap DrawToBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bmp);
                Brush blockBrush = new SolidBrush(Color.Blue);
                Brush textBrush = new SolidBrush(Color.Black);

                string toWriteString = this.declared ? this.name.ToString() : this.value.ToString();
                var sizeOfString = g.MeasureString(toWriteString, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));


                g.FillRectangle(blockBrush, 0, 0, Width, Height);
                g.DrawString(this.declared ? this.name.ToString() : this.value.ToString(), 
                    new Font("Arial", (int)(this.textHeightMulti / 1.333333)), 
                    textBrush, 
                    new Rectangle(new Point(0,0), new Size((int)sizeOfString.Width+1,(int)sizeOfString.Height+1)));
                return bmp;
            }
        }

        public override Block GetSelectedBlock(Point point)
        {
            if (BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, Width, Height)))
                return this;
            return null;
        }

        public override void FitBlockByPoint(Point position, Block block)
        {
            return;
        }
    }
}
