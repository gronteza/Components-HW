using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }
        private FullAdder[] fa;

        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            fa = new FullAdder[Size];

            for (int i=0;i<Size;i++)
            {
                fa[i] = new FullAdder();
                fa[i].ConnectInput1(Input1[i]);
                fa[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(fa[i].Output);
                if (i>0)
                    fa[i].CarryInput.ConnectInput(fa[i-1].CarryOutput);
            }   
            fa[0].CarryInput.Value = 0;
        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            WireSet ws = new WireSet(Size);
            WireSet ws2 = new WireSet(Size);
            MultiBitAdder mba;
            for (int i=0;i<Size;i++)
            {
                for (int j=0;j<Size;j++)
                {
                    mba = new MultiBitAdder(Size);
                    ws.SetValue(i);
                    ws2.SetValue(j);
                    mba.Input1.ConnectInput(ws);
                    mba.Input2.ConnectInput(ws2);
                
                    if (mba.Output.GetValue() != (ws.GetValue() + ws2.GetValue()))
                        return false;
                }
            }
            return true;

        }
    }
}
