using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        //your code here
        private AndGate andGate;

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            Output = calc(this.m_wsInput[iInputCount - 1], this.m_wsInput[iInputCount - 2], iInputCount - 3);
        }

        private Wire calc(Wire input1,Wire input2, int wireNum)
        {
            andGate = new AndGate();
            andGate.ConnectInput1(input1);
            andGate.ConnectInput2(input2);
            
            if (wireNum < 0)
            {
                return andGate.Output;
            }
            return calc(andGate.Output, this.m_wsInput[wireNum], wireNum - 1);
        }

        private WireSet InitTestVariables(int numToBin)
        {
            // Init the wireset for the test 
            String strBinary = Convert.ToString(numToBin, 2);
            WireSet wsTest = new WireSet(m_wsInput.Size);
            Wire wireToConnect = new Wire();
            strBinary = strBinary.PadLeft(m_wsInput.Size, '0');

            for (int i = 0; i< wsTest.Size; i++)
                wsTest[i].Value = strBinary.ToCharArray()[wsTest.Size-i-1] - '0';
            return wsTest;
        }
        public override bool TestGate()
        {
            MultiBitAndGate mbag;

            int outCome;
            
            for (int j = 0; j < Math.Pow(2,m_wsInput.Size); j++)
            {
                outCome = 1;
                mbag = new MultiBitAndGate(m_wsInput.Size);
                mbag.m_wsInput.ConnectInput(InitTestVariables(j));
                for (int i = 0; i < mbag.m_wsInput.Size; i++)
                {
                    if (mbag.m_wsInput[i].Value == 0)
                    {
                        outCome = 0;
                        break;
                    }
                }
                // UNCOMMENT THIS LINE TO SEE THE DEBUG PRINT
                //System.Console.WriteLine("    Testing input " + Convert.ToString(j, 2).PadLeft(m_wsInput.Size, '0') + " -> " + mbag.Output.Value.ToString() + " , " + outCome.ToString());
                if (outCome != mbag.Output.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
