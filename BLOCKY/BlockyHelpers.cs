using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections;


namespace BlockyAPI.BLOCKY
{
    public static class BlockyHelpers
    {
        #region Enums
        public enum BlockReturnType
        {
            String,
            Integer,
            Real,
            Bool,
            Inherits,
            Array,
            Void
        };
        public enum BlockCategoryType
        {
            Math,
            Operations,
            Conditionals,
            Functions,
            InputOutput,
            Other
        };
        public enum BlockMathType
        {
            SIN,
            COS,
            TAN,
            HYPOT,
            ARCCOS,
            ARCSIN,
            ARCTAN,
            HYPERBOLIC_TANGENT,
            HYPERBOLIC_COSINE,
            ABSOLUTE,
            FLOOR,
            CEIL,
            SQRT,
            POW,
        }
        public enum BlockConditionalType
        {
            IF,
            WHILE,
            ELSE_IF,
            ELSE,
        }
        public enum BlockOperationType
        {
            ADDITION,
            SUBTRACTION,
            DIVISION,
            MULTIPLICATION,
            MODULUS,
            ASSIGNMENT,
            BOOL_EQUALITY,
            BOOL_NOT_EQUALITY,
            BOOL_RIGHT_EQUALITY,
            BOOL_LEFT_EQUALITY,
            BOOL_RIGHT,
            BOOL_LEFT,
            BOOL_AND,
            BOOL_OR,
            BITWISE_AND,
            BITWISE_OR,
            BITWISE_LEFT_SHIFT,
            BITWISE_RIGHT_SHIFT,
            BITWISE_XOR,
            BITWISE_COMPLEMENT,
        }
        public enum BlockInputOutputType
        {
            Input,
            Output,
        }
        #endregion

        #region Hashes

        #region Conditional Hashed
        public static Hashtable hashConditional = new Hashtable() {

            {BlockConditionalType.IF,
                new BlockScheme("if",1,BlockCategoryType.Conditionals,
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Integer }
                    )},

            {BlockConditionalType.ELSE_IF,
                new BlockScheme("else if",1, BlockCategoryType.Conditionals,
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Integer }
                    )},

            {BlockConditionalType.ELSE,
                new BlockScheme("else",1, BlockCategoryType.Conditionals,
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Integer }
                    )},

            {BlockConditionalType.WHILE,
                new BlockScheme("while",1, BlockCategoryType.Conditionals,
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Integer }
                    )},
        };
        #endregion

        #region Operators Hashed
        public static Hashtable hashOperations = new Hashtable() {

            {BlockOperationType.ADDITION,
                new BlockScheme("+",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},

            {BlockOperationType.ASSIGNMENT,
                new BlockScheme("=",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},

            {BlockOperationType.BITWISE_AND,
                new BlockScheme("&",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer, BlockReturnType.Integer }
                    )},

            {BlockOperationType.BITWISE_COMPLEMENT,
                new BlockScheme("~",1, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BITWISE_LEFT_SHIFT,
                new BlockScheme("<<",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BITWISE_OR,
                new BlockScheme("|",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer, BlockReturnType.Integer }
                    )},

            {BlockOperationType.BITWISE_RIGHT_SHIFT,
                new BlockScheme(">>",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BITWISE_XOR,
                new BlockScheme("^",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BOOL_AND,
                new BlockScheme("&&",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BOOL_EQUALITY,
                new BlockScheme("==",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }

                    )},

            {BlockOperationType.BOOL_LEFT,
                new BlockScheme("<",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }

                    )},

            {BlockOperationType.BOOL_LEFT_EQUALITY,
                new BlockScheme("<=",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }
                    )},

            {BlockOperationType.BOOL_NOT_EQUALITY,
                new BlockScheme("!=",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }
                    )},

            {BlockOperationType.BOOL_OR,
                new BlockScheme("||",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer }
                    )},

            {BlockOperationType.BOOL_RIGHT,
                new BlockScheme(">",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }
                    )},


            {BlockOperationType.BOOL_RIGHT_EQUALITY,
                new BlockScheme(">=",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Bool,BlockReturnType.Bool,BlockReturnType.Bool }
                    )},


            {BlockOperationType.DIVISION,
                new BlockScheme("/",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},

            {BlockOperationType.MODULUS,
                new BlockScheme("%",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},

            {BlockOperationType.MULTIPLICATION,
                new BlockScheme("*",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},

            {BlockOperationType.SUBTRACTION,
                new BlockScheme("-",2, BlockCategoryType.Operations,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real },
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.String,BlockReturnType.String,BlockReturnType.String }
                    )},
        };
        #endregion

        #region Math Hashed
        public static Hashtable hashMath = new Hashtable() {
            {BlockMathType.ABSOLUTE,
                new BlockScheme("abs",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Integer}
                    )},
            {BlockMathType.ARCCOS,
                new BlockScheme("acos",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.ARCSIN,
                new BlockScheme("asin",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.ARCTAN,
                new BlockScheme("atan",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.CEIL,
                new BlockScheme("ceil",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Real}
                    )},
            {BlockMathType.COS,
                new BlockScheme("cos",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.FLOOR,
                new BlockScheme("floor",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Integer,BlockReturnType.Real}
                    )},
            {BlockMathType.HYPERBOLIC_COSINE,
                new BlockScheme("cosh",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.HYPERBOLIC_TANGENT,
                new BlockScheme("tanh",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.HYPOT,
                new BlockScheme("hypot",2,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Integer}
                    )},
            {BlockMathType.POW,
                new BlockScheme("pow",2,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real,BlockReturnType.Integer},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer,BlockReturnType.Integer}
                    )},
            {BlockMathType.SIN,
                new BlockScheme("sin",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.SQRT,
                new BlockScheme("sqrt",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
            {BlockMathType.TAN,
                new BlockScheme("tan",1,BlockCategoryType.Math,
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Real},
                    new BlockReturnType[]{BlockReturnType.Real,BlockReturnType.Integer}
                    )},
        };
        #endregion

        #region Input/Output Hashed
        //Output soon to be
        public static Hashtable hashInputOutput = new Hashtable() {

            {BlockInputOutputType.Input,
                new BlockScheme("get",1,BlockCategoryType.InputOutput,
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Bool },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Integer },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.String },
                    new BlockReturnType[]{BlockReturnType.Void,BlockReturnType.Real }
                    )},

        };
        #endregion

        #endregion

        #region Helper Functions
        //This function checks a list of parameters for errors 
        //and nulls and adds them to the list of exceptions implemented using BlockException class
        public static List<BlockException> CheckForErrorsAndNulls(List<Block> blocks, bool checkCombinations = true)
        {
            List<BlockException> errors = new List<BlockException>();
            for (int i = 0; i < blocks.Count(); i++)
            {
                if (blocks[i] != null)
                {
                    errors.AddRange(blocks[i].CheckForErrors);
                    if(blocks[i].GetReturnTypes().Count==0 && checkCombinations)
                    {
                        if (blocks[i].GetType() == typeof(BlockOutput)) continue;
                        errors.Add(new BlockException(blocks[i], "Invalid combination of parameter types"));
                    }
                }
                else
                {
                    errors.Add(new BlockException(blocks[i], "Missing parameter"));
                }
            }
            return errors;
        }

        public static List<BlockReturnType> ComputeReturns(BlockScheme scheme, List<Block> parameters)
        {
            //DOES NOT CATCH STRING IN WHILE
            if (scheme == null)
                return new List<BlockReturnType>();
            List<BlockReturnType> returns = new List<BlockReturnType>();
            List<List<BlockReturnType>> returnsParams = new List<List<BlockReturnType>>();
            parameters.ForEach((param) => returnsParams.Add(param.GetReturnTypes()));
            scheme.resultingPermutations.ForEach((types) => {
                bool isOkPermutation = true;
                for (int i = 0; i < scheme.numOfParams; i++)
                {
                    bool hasIt = false;
                    returnsParams[i].ForEach((returned) => hasIt = hasIt ? true : returned == types[i + 1]);
                    if (!hasIt)
                        isOkPermutation = false;
                }
                if (isOkPermutation)
                    returns.Add(types[0]);
            });
            return returns;
        }
        #endregion
    }
}
