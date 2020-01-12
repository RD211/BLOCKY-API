using System;
using System.Collections.Generic;
using System.Drawing;
using BlockyAPI.BLOCKY.Function_Blocks;
using BlockyAPI.BLOCKY.Operation_Blocks;
using static BlockyAPI.BLOCKY.BlockyHelpers;

namespace BlockyAPI.BLOCKY
{
    class BlockyInterface
    {
        public MainBlockSpace space;
        public BlockyInterface()
        {
            //TODO Init variables
            BlockInteger integerBlock = new BlockInteger("MyAge","0",true);
            BlockInteger integerBlockValue = new BlockInteger("MyAge", "16", false);
            BlockInteger integerBlock2 = new BlockInteger("MyAge", "18", false);
            BlockInteger integerBlock3 = new BlockInteger("MyAge", "1", false);

            BlockString stringBlock = new BlockString("MyName","",true);
            BlockString stringBlockValue = new BlockString("MyName", "David", false);
            BlockString stringSpaceBlock = new BlockString("spacer", " ", false);

            BlockOperation blockAssignmentInteger = new BlockOperation(BlockOperationType.ASSIGNMENT);
            blockAssignmentInteger.ChangeParameter(integerBlock, 0);
            blockAssignmentInteger.ChangeParameter(integerBlockValue, 1);


            
            BlockOperation blockAssignmentString = new BlockOperation(BlockOperationType.ASSIGNMENT);
            blockAssignmentString.ChangeParameter(stringBlock, 0);
            blockAssignmentString.ChangeParameter(stringBlockValue, 1);

            BlockOperation compareOperation = new BlockOperation(BlockOperationType.BOOL_LEFT);
            compareOperation.ChangeParameter(integerBlock, 0);
            compareOperation.ChangeParameter(integerBlock2, 1);

            BlockOperation decrementOperation = new BlockOperation(BlockOperationType.SUBTRACTION);
            decrementOperation.ChangeParameter(integerBlock, 0);
            decrementOperation.ChangeParameter(integerBlock3, 1);

            BlockOperation decrementAssign = new BlockOperation(BlockOperationType.ASSIGNMENT);
            decrementAssign.ChangeParameter(integerBlock, 0);
            decrementAssign.ChangeParameter(decrementOperation, 1);

            BlockFunctionCall blockInput = new BlockFunctionCall(BlockInputOutputType.Input);
            blockInput.ChangeParameter(stringBlock, 0);
            BlockOperation decrementSecondOperation = new BlockOperation(BlockOperationType.ASSIGNMENT);
            decrementSecondOperation.ChangeParameter(integerBlock, 0);
            decrementSecondOperation.ChangeParameter(decrementOperation, 1);
            BlockConditional blockConditional2 = new BlockConditional(BlockConditionalType.WHILE);
            blockConditional2.ChangeParameter(integerBlock, 0);
            blockConditional2.ChangeInstruction(decrementAssign, 0);
            blockConditional2.ChangeInstruction(blockInput, 1);
            BlockConditional blockConditional = new BlockConditional(BlockConditionalType.WHILE);
            blockConditional.ChangeParameter(integerBlock,0);
            blockConditional.ChangeInstruction(decrementAssign, 0);
            blockConditional.ChangeInstruction(blockInput, 1);
            blockConditional.ChangeInstruction(blockConditional2, 2);
            space = new MainBlockSpace();
            space.functions.Add(new BlockMainFunction());
            space.functions[0].Variables.Add(integerBlock);
            space.functions[0].Variables.Add(stringBlock);
            space.functions[0].ChangeInstruction(blockAssignmentInteger,0);
            space.functions[0].ChangeInstruction(blockAssignmentString, 1);
            space.functions[0].ChangeInstruction(blockConditional, 2);

            space.functions[0].ChangeInstruction(blockInput, 3);
            space.functions[0].ChangeInstruction(blockConditional, 4);


        }
        public List<BlockException> tempRep = new List<BlockException>();
        public String Response()
        {
            List<BlockException> errorList = space.CheckForErrors;
            this.tempRep = errorList;
            return errorList.Count == 0 ? space.ConvertToCPlusPlus: null;
        }
        public Bitmap GetBitmapOfProgram()
        {
            return space.DrawToBitmap;
        }
        public Block GetSelectedBlock(Point position)
        {
            return space.GetSelectedBlock(position);
        }
    }
}
