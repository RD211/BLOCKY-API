using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BlockyAPI.BLOCKY
{
    public class BlockOutput : Block
    {
        #region Constructor
        public BlockOutput() =>
            //Init params with empty list
            this.parameters = new List<Block>();
        #endregion

        #region Error Checking
        //Interface implementation of inherited class function to check for errors before compiling to c++
        public override List<BlockException> CheckForErrors
        {
            get
            {
                //Checks if output block does not have params
                if (this.parameters.Count() == 0)
                {
                    return new List<BlockException>() { new BlockException(this, "Missing parameter!") };
                }
                //Checks if parameters are null or have erros, called with false to not check for type,
                //cause it does not matter unless they are void or array
                return BlockyHelpers.CheckForErrorsAndNulls(this.parameters, false);
            }
        }

        #endregion

        #region Convert to C++
        //Interface implementation of inherited class function to compile to c++ after getting checked
        public override string ConvertToCPlusPlus
        {
            get
            {
                String response = "cout";
                //Iterates over all params and adds them
                foreach (var v in this.parameters)
                {
                    response += "<<" + "(" + v.ConvertToCPlusPlus + ")";
                }
                return response;
            }
        }


        #endregion
        public override int Width
        {
            get
            {
                int width = this.scheme.text.Length * 4;
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
                int maxim = 10;
                this.parameters.ForEach((par) => maxim = Math.Max(maxim, par.Height));
                return maxim;
            }
        }

        public override Bitmap DrawToBitmap => throw new NotImplementedException();

        public override void FitBlockByPoint(Point position, Block block)
        {
            throw new NotImplementedException();
        }

        public override Block GetSelectedBlock(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
