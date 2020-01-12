using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    public class BlockConditional : Block
    {
        #region Constructor
        //Constructor 
        //1.Takes BlockConditionalType parameter and returns hashed scheme,
        //2.Inits inherited values
        public BlockConditional(BlockConditionalType type)
        {
            this.scheme = ((BlockScheme)hashConditional[type]);
            this.parameters = new List<Block>();
            this.instructions = new List<Block>();
            for (int i = 0; i < scheme.numOfParams; i++)
                parameters.Add(null);
        }
        #endregion

        #region Error checking
        public override List<BlockException> CheckForErrors
        {
            get
            {
                List<BlockException> errors = BlockyHelpers.CheckForErrorsAndNulls(this.parameters);
                errors.AddRange(BlockyHelpers.CheckForErrorsAndNulls(this.instructions, false));
                return errors;
            }
        }
        #endregion

        #region Convert to C++
        //Converts object to C++
        public override string ConvertToCPlusPlus
        {
            get
            {
                String code = scheme.text + "(" + this.parameters[0].ConvertToCPlusPlus + "){" + '\n';
                instructions.ForEach((ins) => code += ins.ConvertToCPlusPlus + ";" + '\n');
                code += "}";
                return code;
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
                this.instructions.ForEach((ins) => width = Math.Max(width, ins.Width));
                return width+5;
            }
        }

        public override int Height
        {
            get
            {
                Graphics g = Graphics.FromImage(new Bitmap(10, 10));
                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                int sumOfIns = (int)sizeOfString.Height+1;
                this.instructions.ForEach((ins) => sumOfIns += ins.Height);
                return sumOfIns+10;
            }
        }

        public override Bitmap DrawToBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bmp);
                var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                Brush textBrush = new SolidBrush(Color.Black);
                Brush blockBrush = new SolidBrush(Color.Green);
                Pen blockOutLinePen = new Pen(Color.Black)
                {
                    Width = 5
                };
                g.FillRectangle(blockBrush, 0, 0, 10, Height);
                g.FillRectangle(blockBrush, 0, Height-10, 190, 10);
                g.DrawRectangle(blockOutLinePen, 0, Height - 10, 190, 10);

                g.FillRectangle(blockBrush, new Rectangle(0,0,(int)sizeOfString.Width+1,(int)sizeOfString.Height+1));
                g.DrawString(scheme.text, new Font("Arial", (int)(this.textHeightMulti/ 1.333333)), textBrush, new Rectangle(new Point(0,0), new Size((int)sizeOfString.Width+1, (int)sizeOfString.Height + 1)));
                g.DrawImage(this.parameters[0].DrawToBitmap, new Point((int)sizeOfString.Width + 1, 0));
                int runningSum = (int)sizeOfString.Height + 1;
                for (int i = 0; i < this.instructions.Count; i++)
                {
                    this.instructions[i].position = new Point(this.position.X+ 10,this.position.Y + runningSum);
                    var pos = new Point(10, runningSum);
                    g.DrawImage(this.instructions[i].DrawToBitmap, pos);
                    runningSum += this.instructions[i].Height;
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
            for (int i = 0; i < this.parameters.Count; i++)
            {
                if (BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X + runningSum, 0)) <= 5.0)
                {
                    this.parameters[i] = block;
                    block.fatherBlock = this;
                }
                runningSum += this.parameters[i].Width;
            }
            if (BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X + 10, this.position.Y + Height - 5)) <= 5.0)
            {
                this.instructions.Add(block);
                block.fatherBlock = this;
            }
            else
            {
                this.parameters.ForEach((par) => par.FitBlockByPoint(position, block));
                this.instructions.ForEach((ins) => ins.FitBlockByPoint(position, block));
            }
        }

        public override Block GetSelectedBlock(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizeOfString = g.MeasureString(scheme.text, new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
            if (BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, 10, Height)) ||
                   BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X,this.position.Y+ Height - 10, 190, 10)) ||
                   BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, (int)sizeOfString.Width + 1, (int)sizeOfString.Height + 1)))
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
                this.instructions.ForEach((ins) =>
                {
                    if (ins.GetSelectedBlock(point) != null)
                        block = ins.GetSelectedBlock(point);
                });
                return block;
            }
        }
    }
}
