using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockyAPI.BLOCKY.Function_Blocks
{
    class BlockMainFunction : Block
    {
        public List<BlockVariable> Variables = new List<BlockVariable>();
        public BlockMainFunction()
        {
            this.instructions = new List<Block>();
        }
        public String AddVariable(BlockVariable block, int place)
        {
            instructions.Add(block);
            return "OK";
        }
        public String RemoveInstructionAt(int place)
        {
            if (place < 0 || place >= instructions.Count())
            {
                return "FAILED INVALID INDEX";
            }
            instructions.RemoveAt(place);
            return "";
        }
        public override List<BlockException> CheckForErrors => BlockyHelpers.CheckForErrorsAndNulls(this.instructions, true);

        public override string ConvertToCPlusPlus
        {
            get
            {
                String code = "int main(){" + '\n';
                foreach (var v in Variables)
                {
                    if (v.GetType() == typeof(BlockString))
                    {
                        code += "string " + v.name + " = " + '"' + ((BlockString)v).value + '"' + ';' + '\n';
                    }
                    else if (v.GetType() == typeof(BlockInteger))
                    {
                        code += "long long " + v.name + " = " + ((BlockInteger)v).value + ";" + '\n';
                    }
                    else if (v.GetType() == typeof(BlockArray))
                    {
                        code += "long long" + v.name + "[10000];" + '\n';
                    }
                    else
                    {
                        code += "double " + " = " + ((BlockReal)v).value + ";" + '\n';
                    }
                }
                foreach (var line in instructions)
                {
                    code += line.ConvertToCPlusPlus + (line.GetType() != typeof(BlockConditional) ? ';' : ' ') + '\n';
                }
                code += "}";
                return code;
            }
        }

        public override int Width
        {
            get
            {
                //normal height of a block not affected by others is 10 but when height of block gets over 10
                //it changes to that item thus the max between 10 and the parameters height is the blocks height
                Graphics g = Graphics.FromImage(new Bitmap(5, 5));
                var sizeOfString = g.MeasureString("main function", new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                int maxim = (int)sizeOfString.Width+1;

                this.instructions.ForEach((ins) => { maxim = Math.Max(maxim, ins.Width); });
                return maxim+20;
            }
        }

        public override int Height
        {
            get
            {
                Graphics g = Graphics.FromImage(new Bitmap(5, 5));
                var sizeOfString = g.MeasureString("main function", new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
                int sumOfIns = (int)sizeOfString.Height+1;
                this.instructions.ForEach((ins) => sumOfIns += ins.Height);
                return sumOfIns+this.textHeightMulti;
            }
        }


        public override Bitmap DrawToBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bmp);
                Brush textBrush = new SolidBrush(Color.Black);
                Brush blockBrush = new SolidBrush(Color.Orange);
                var sizeOfString = g.MeasureString("main function", new Font("Arial", (int)(this.textHeightMulti / 1.333333)));

                g.FillRectangle(blockBrush, 0, 0, (int)sizeOfString.Width+1, (int)sizeOfString.Height+1);
                g.FillRectangle(blockBrush, 0, 0, 10, this.Height);
                g.FillRectangle(blockBrush, 0, this.Height-10, 50, (int)sizeOfString.Height+1);
                g.DrawString("main function",new Font("Arial", (int)(this.textHeightMulti / 1.333333)), textBrush, new Rectangle(new Point(0,0), new Size((int)sizeOfString.Width+1,(int)sizeOfString.Height+1)));
                int runningSum = (int)sizeOfString.Height+1;
                for(int i = 0;i<this.instructions.Count;i++)
                {
                    this.instructions[i].position = new Point(this.position.X + 10, this.position.Y + runningSum);
                    var pos =  new Point(10,runningSum);
                    g.DrawImage(this.instructions[i].DrawToBitmap, pos);
                    runningSum += this.instructions[i].Height;
                }
                return bmp;
            }
        }

        public override Block GetSelectedBlock(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizeOfString = g.MeasureString("main function", new Font("Arial", (int)(this.textHeightMulti / 1.333333)));
            if(BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, (int)sizeOfString.Width + 1, (int)sizeOfString.Height + 1)) ||
                    BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y, 10, this.Height)) ||
                    BlockyDrawingHelpers.IsPointInsideRectangle(point, new Rectangle(this.position.X, this.position.Y + this.Height - 10, 50, (int)sizeOfString.Height + 1)))
                {
                return this;
            }
            else
            {
                Block block = null;
                this.instructions.ForEach((ins) => {
                    if (ins.GetSelectedBlock(point) != null)
                    {
                        block = ins.GetSelectedBlock(point);
                    }
                });
                return block;
            }
        }

        public override void FitBlockByPoint(Point position, Block block)
        {
            if (BlockyDrawingHelpers.DistanceBetweenTwoPoints(position, new Point(this.position.X + 10, this.position.Y + Height - 5))<=5.0)
            {
                this.instructions.Add(block);
                block.fatherBlock = this;
            }
            else
            {
                this.instructions.ForEach((ins) => ins.FitBlockByPoint(position, block));
            }
        }
    }
}