using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        //your code here
        private OrGate[] m_gOrGate;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            //your code here
            m_gOrGate = new OrGate[iSize];

            WireSet ws_resault = new WireSet(iSize);

            //your code here
            for (int i = 0; i < iSize; i++)
            {
                m_gOrGate[i] = new OrGate();
                m_gOrGate[i].ConnectInput1(Input1[i]);
                m_gOrGate[i].ConnectInput2(Input2[i]);

                ws_resault[i].ConnectInput(m_gOrGate[i].Output);

            }
            Output.ConnectInput(ws_resault);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(or)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
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

        private WireSet InitRandTestVar(int testSize)
        {
            WireSet wsTest = new WireSet(testSize);
            Random rnd = new Random();
            for (int i = 0; i < testSize; i++)
            {
                wsTest[i].Value = rnd.Next(0, 2);
            }
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
            BitwiseOrGate bwog;
            OrGate m_gLocalOr;

            for (int j = 0; j < Math.Pow(2, Size); j++)
            {
                bwog = new BitwiseOrGate(Size);
                bwog.Input1.ConnectInput(InitTestVariables(j));
                bwog.Input2.ConnectInput(InitRandTestVar(Size));

                for (int i = 0; i < Size; i++)
                {
                    m_gLocalOr = new OrGate();
                    m_gLocalOr.ConnectInput1(bwog.Input1[i]);
                    m_gLocalOr.ConnectInput2(bwog.Input2[i]);
                    if (bwog.Input1[i].Value == 1 || bwog.Input2[i].Value == 1)
                    {
                        if (m_gLocalOr.Output.Value != 1 || bwog.m_gOrGate[i].Output.Value != 1)
                            return false;
                    }
                    else
                    {
                        if (m_gLocalOr.Output.Value != 0)
                            return false;
                    }
                }
                // UNCOMMENT THIS LINES TO SEE THE DEBUG PRINT
                //System.Console.WriteLine("    Testing input1 " + " -> " + WStoString(bwog.Input1));
                //System.Console.WriteLine("    Testing input2 " + " -> " + WStoString(bwog.Input2));
                //System.Console.WriteLine("    Testing output " + " -> " + WStoString(bwog.Output));
            }
            return true;
        }
    }
}
