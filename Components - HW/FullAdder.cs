using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        //your code here
        HalfAdder ha;
        HalfAdder ha2;
        OrGate og;

        public FullAdder()
        {
            CarryInput = new Wire();

            //your code here
            ha = new HalfAdder();
            ha2 = new HalfAdder();
            og = new OrGate();

            ha.ConnectInput1(Input1);
            ha.ConnectInput2(Input2);
            ha2.ConnectInput1(ha.Output);
            ha2.ConnectInput2(CarryInput);
            og.ConnectInput1(ha2.CarryOutput);
            og.ConnectInput2(ha.CarryOutput);

            Output = ha2.Output;
            CarryOutput = og.Output;
        }
        
        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
             if (Output.Value != 0 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 1;
             if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 0;
             if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 0;
             if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 0;
             if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 1;
             if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 1;
             if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
             if (Output.Value != 1 || CarryOutput.Value != 1)
                return false;
            return true;
        }
    }
}
