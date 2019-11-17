using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here
        private NotGate[] m_gNotGate;

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            m_gNotGate = new NotGate[iSize];
            WireSet ws_resault = new WireSet(iSize);

            for (int i = 0; i < iSize; i++)
            {
                m_gNotGate[i] = new NotGate();
                m_gNotGate[i].ConnectInput(Input[i]);

                ws_resault[i].ConnectInput(m_gNotGate[i].Output);
            }
            Output.ConnectInput(ws_resault);
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        private WireSet InitTestVariables(int numToBin)
        {
            // Init the wireset for the test 
            String strBinary = Convert.ToString(numToBin, 2);
            WireSet wsTest = new WireSet(Size);
            Wire wireToConnect = new Wire();
            strBinary = strBinary.PadLeft(Size, '0');

            for (int i = 0; i < wsTest.Size; i++)
                wsTest[i].Value = strBinary.ToCharArray()[wsTest.Size - i - 1] - '0';
            return wsTest;
        }

        private String WStoString(WireSet ws)
        {
            String str = "";

            for (int i = 0; i < ws.Size; i++)
            {
                str += ws[i].Value.ToString();
            }
            return str;

        }

        public override bool TestGate()
        {
            BitwiseNotGate bwng;

            for (int j = 0; j < Math.Pow(2, Size); j++)
            {
                bwng = new BitwiseNotGate(Size);
                bwng.Input.ConnectInput(InitTestVariables(j));

                for (int i = 0; i < Size; i++)
                {
                    if (bwng.Input[i].Value == bwng.Output[i].Value)
                    {
                        return false;
                    }
                }
                // UNCOMMENT THIS LINES TO SEE THE DEBUG PRINT
                //System.Console.WriteLine("    Testing input  " + " -> " + WStoString(bwng.Input));
                //System.Console.WriteLine("    Testing output " + " -> " + WStoString(bwng.Output));
            }
            return true;
        }

    }
}
