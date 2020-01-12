using System.Collections.Generic;
using System.Linq;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    public class BlockScheme
    {
        #region Variables
        public string text; //Text to write example "abs", "sqrt"
        public BlockCategoryType categoryType; //Category of block
        public List<List<BlockReturnType>> resultingPermutations = new List<List<BlockReturnType>>(); //Permutations for type checking
        public int numOfParams; //Number of parameters accepted by the function
        #endregion

        #region Constructor
        public BlockScheme(string text, int numOfParams, BlockCategoryType categoryType, params BlockReturnType[][] permutations)
        {
            this.text = text;
            this.numOfParams = numOfParams;
            this.categoryType = categoryType;
            foreach(var v in permutations) { 
                resultingPermutations.Add(v.ToList());
            }
        }
        #endregion
    }

    //In works
    /*
    public class BlockPermittedTypeTemplate
    {
        int templateNumber;
        BlockReturnType[] types;
        public BlockPermittedTypeTemplate(int templateNumber, params BlockReturnType[] types)
        {
            this.templateNumber = templateNumber;
            this.types = types;
        }
    }
    */
}
