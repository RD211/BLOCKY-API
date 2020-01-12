using BlockyAPI.BLOCKY.Function_Blocks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockyAPI.BLOCKY
{
    class MainBlockSpace : Block
    {
        public List<BlockVariable> variables = new List<BlockVariable>();
        public List<BlockMainFunction> functions = new List<BlockMainFunction>();
        public void AddFunction(BlockMainFunction main)
        {
            functions.Add(main);
        }
        public void AddVariable(BlockVariable var)
        {
            variables.Add(var);
        }

        public override Block GetSelectedBlock(Point point)
        {
                Block block = null;
                this.functions.ForEach((f) => {
                    Block temp = f.GetSelectedBlock(point);
                    if (temp != null)
                        block = temp;
                });
            this.instructions.ForEach((ins) => {
                Block temp = ins.GetSelectedBlock(point);
                if (temp != null)
                    block = temp;
            });
            return block;
        }

        public override void FitBlockByPoint(Point position, Block block)
        {
            this.functions.ForEach((func) => func.FitBlockByPoint(position, block));
            this.instructions.ForEach((ins) => ins.FitBlockByPoint(position, block));
        }

        public override List<BlockException> CheckForErrors
        {
            get
            {
                List<BlockException> errors = new List<BlockException>();
                functions.ForEach((f) => errors.AddRange(f.CheckForErrors));
                return errors;
            }
        }

        public override string ConvertToCPlusPlus
        {
            get
            {
                String code =
    @"#include <bits/stdc++.h>
using namespace std;
";
                foreach (var v in variables)
                {
                    if (v.GetType() == typeof(BlockString))
                    {
                        code += "string " + v.name + " = " + '"' + ((BlockString)v).value + '"' + ';' + '\n';
                    }
                    else if (v.GetType() == typeof(BlockInteger))
                    {
                        code += "long long " + " = " + ((BlockInteger)v).value + ";" + '\n';
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
                foreach (var line in functions)
                {
                    code += line.ConvertToCPlusPlus + '\n';
                }
                return code;

            }
        }

        public override int Width => 1000;

        public override int Height => 1000;

        public override Bitmap DrawToBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bmp);
                this.functions.ForEach((f) => g.DrawImage(f.DrawToBitmap, f.position));
                this.instructions.ForEach((ins) => g.DrawImage(ins.DrawToBitmap, ins.position));

                return bmp;
            }
        }
    }
}
