using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockyAPI.BLOCKY
{
    public class BlockInteger : BlockVariable
    {
        #region Constructor
        //Takes name, default value and if the variable is declared or if its used inline
        public BlockInteger(String name, String value,bool declared)
        {
            this.name = name;
            this.value = value;
            this.declared = declared;
        }
        #endregion

        #region Convert to C++
        //Returns name of variable if it is declared otherwise
        //returns value of variable as its used inline
        public override string ConvertToCPlusPlus => declared ? name : value;
        #endregion
    }
}
