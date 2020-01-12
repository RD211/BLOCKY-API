using System;

namespace BlockyAPI.BLOCKY
{
    public class BlockString : BlockVariable
    {

        #region Constructor
        //Takes name, default value and if it is declared or if its used inline
        public BlockString(String name, String value, bool declared)
        {
            this.name = name;
            this.value = value;
            this.declared = declared;
        }
        #endregion

        #region Convert to C++
        //Returns name of variable if it is used as a variable => is declared,
        //otherwise returns value as it is used inline and there exist no variable.
        public override string ConvertToCPlusPlus => this.declared ? name : '"' + value + '"';
        #endregion
    }
}
