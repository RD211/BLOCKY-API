using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockyAPI.BLOCKY
{
    public class BlockReal : BlockVariable
    {
        #region Constructor
        public BlockReal(String name, String value, bool declared)
        {
            this.name = name;
            this.value = value;
            this.declared = declared;
        }
        #endregion

        #region Convert to C++
        public override string ConvertToCPlusPlus => declared ? name : value;
        #endregion
    }
}
