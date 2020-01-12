using System;
using System.Collections.Generic;
using System.Drawing;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    public abstract class Block
    {
        #region Variables
        public List<Block> parameters = new List<Block>(), instructions = new List<Block>();
        protected BlockScheme scheme;
        public Block fatherBlock;
        protected BlockException exception;
        public Point position = new Point(0, 0);
        protected int textWidthMulti = 18;
        protected int textHeightMulti = 30;
        #endregion

        #region Abstract Functions
        public abstract string ConvertToCPlusPlus { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract Block GetSelectedBlock(Point point);

        public abstract void FitBlockByPoint(Point position, Block block);
        public abstract Bitmap DrawToBitmap { get; }

        public abstract List<BlockException> CheckForErrors { get; }
        #endregion

        #region Normal Functions
        public virtual List<BlockReturnType> GetReturnTypes() => BlockyHelpers.ComputeReturns(scheme, parameters);
        public void ChangeParameter(Block block, int position)
        {
            if (parameters.Count <= position)
                parameters.Add(block);
            else
                this.parameters[position] = block;
            block.fatherBlock = this;
        }
        public void ChangeInstruction(Block block, int position)
        {
            if (instructions.Count <= position)
                instructions.Add(block);
            else
                this.instructions[position] = block;
            block.fatherBlock = this;
        }
        public void SetException(BlockException e)
        {
            this.exception = e;
        }

        public List<int> GetListOfWidths()
        {
            List<int> widths = new List<int>();
            parameters.ForEach((par) => widths.Add(par.Width));
            return widths;
        }
        public List<int> GetListOfHeights()
        {
            List<int> heights = new List<int>();
            parameters.ForEach((par) => heights.Add(par.Height));
            return heights;
        }
        #endregion
    }
}
