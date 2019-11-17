using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        //your code here
        private OrGate orGate;

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            Output = calc(this.m_wsInput[iInputCount - 1], this.m_wsInput[iInputCount - 2], iInputCount - 3);
        }

        private Wire calc(Wire input1, Wire input2, int wireNum)
        {
            orGate = new OrGate();
            orGate.ConnectInput1(input1);
            orGate.ConnectInput2(input2);

            if (wireNum < 0)
            {
                return orGate.Output;
            }
            return calc(orGate.Output, this.m_wsInput[wireNum], wireNum - 1);
        }

        private WireSet InitTestVariables(int numToBin)
        {
            // Init the wireset for the test 
            String strBinary = Convert.ToString(numToBin, 2);
            WireSet wsTest = new WireSet(m_wsInput.Size);
            Wire wireToConnect = new Wire();
            strBinary = strBinary.PadLeft(m_wsInput.Size, '0');

            for (int i = 0; i < wsTest.Size; i++)
                wsTest[i].Value = strBinary.ToCharArray()[wsTest.Size - i - 1] - '0';
            return wsTest;
        }
        public override bool TestGate()
        {
            MultiBitOrGate mbog;

            int outCome;

            for (int j = 0; j < Math.Pow(2, m_wsInput.Size); j++)
            {
                outCome = 0;
                mbog = new MultiBitOrGate(m_wsInput.Size);
                mbog.m_wsInput.ConnectInput(InitTestVariables(j));
                for (int i = 0; i < mbog.m_wsInput.Size; i++)
                { 
                    if (mbog.m_wsInput[i].Value == 1)
                    {
                        outCome = 1;
                        break;
                    }
                }
                // UNCOMMENT THIS LINE TO SEE THE DEBUG PRINT
                //System.Console.WriteLine("    Testing input " + Convert.ToString(j, 2).PadLeft(m_wsInput.Size, '0') + " -> " + mbog.Output.Value.ToString() + " , " + outCome.ToString());
                if (outCome != mbog.Output.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
