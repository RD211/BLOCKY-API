using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    class BlockFunctionCall : Block
    {
        #region Constructor
        public BlockFunctionCall(BlockMathType type)
        {
            this.parameters = new List<Block>();
            this.scheme = (BlockScheme)hashMath[type];
            for (int i = 0; i < scheme.numOfParams; i++)
                this.parameters.Add(null);
        }
        public BlockFunctionCall(BlockInputOutputType type)
        {
            this.parameters = new List<Block>();
            this.scheme = (BlockScheme)hashInputOutput[type];
            for (int i = 0; i < scheme.numOfParams; i++)
                this.parameters.Add(null);
        }
        #endregion

        #region Check for errors
        public override List<BlockException> CheckForErrors
        {
            get
            {
                List<BlockException> errors = new List<BlockException>();
                bool ok = true;
                parameters.ForEach((par) => { if (par == null) ok = false; });
                if (!ok)
                    errors.Add(new BlockException(this, "Missing input variable"));
                errors.AddRange(BlockyHelpers.CheckForErrorsAndNulls(this.parameters));
                return errors;
            }
        }
        #endregion

        #region Convert to C++
        public override string ConvertToCPlusPlus
        {
            get
            {
                String inside = "";
                parameters.ForEach((par) => inside += par.ConvertToCPlusPlus + ',');
                inside = inside.Remove(inside.Count() - 1);
                return this.scheme.text + "(" + inside + ")";
            }
        }

        #endregion
        public override int Width
        {
            get
            {
                Graphics g = Graphics.FromImage(new Bitmap(10, 10));
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
                Graphics g = Graphics.FromImage(new Bitmap(10, 10));
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
                Brush blockBrush = new SolidBrush(Color.Red);

                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                g.FillRectangle(blockBrush, new Rectangle(0, 0, (int)sizeOfString.Width + 1, (int)sizeOfString.Height + 1));

                g.DrawString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)), textBrush, new Rectangle(new Point(0,0), new Size((int)sizeOfString.Width+1, (int)sizeOfString.Height+1)));
                int runningSum = (int)sizeOfString.Width+1;
                for (int i = 0; i < this.parameters.Count; i++)
                {
                    var p = new Point(runningSum, 0);
                    g.DrawImage(this.parameters[i].DrawToBitmap, p);
                    runningSum += this.parameters[i].Width;
                }
                return bmp;
            }
        }

        public override void FitBlockByPoint(Point position, Block block)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));

            int runningSum = (int)sizeOfString.Width + 1;
            for(int i = 0;i<this.parameters.Count;i++)
            {
                if (BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X + runningSum, 0))<=5.0)
                {
                    this.parameters[i] = block;
                    block.fatherBlock = this;
                }
                runningSum += this.parameters[i].Width;
            }
                this.parameters.ForEach((par) => par.FitBlockByPoint(position, block));
        }

        public override Block GetSelectedBlock(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
            if (BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, (int)sizeOfString.Width + 1, (int)sizeOfString.Height + 1)))
            {
                return this;
            }
            else
            {
                Block block = null;
                this.parameters.ForEach((par) =>
                {
                    if (par.GetSelectedBlock(point) != null)
                        block = par.GetSelectedBlock(point);

                });
                return block;
            }
        }
    }
}
