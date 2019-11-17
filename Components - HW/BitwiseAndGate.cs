using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        //your code here
        private AndGate[] m_gAnd;


        public BitwiseAndGate(int iSize)
            : base(iSize)
        {

            m_gAnd = new AndGate[iSize];

            WireSet ws_resault = new WireSet(iSize);

            //your code here
            for (int i = 0; i < iSize; i++)
            {
                m_gAnd[i] = new AndGate();
                m_gAnd[i].ConnectInput1(Input1[i]);
                m_gAnd[i].ConnectInput2(Input2[i]);

                ws_resault[i].ConnectInput(m_gAnd[i].Output);
                
            }
            Output.ConnectInput(ws_resault);

        }


        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
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
            BitwiseAndGate bwag;
            AndGate m_gLocalAnd; 
            
            for (int j = 0; j < Math.Pow(2, Size); j++)
            {
                bwag = new BitwiseAndGate(Size);
                bwag.Input1.ConnectInput(InitTestVariables(j));
                bwag.Input2.ConnectInput(InitRandTestVar(Size));
                
                for (int i = 0; i < Size; i++)
                {
                    m_gLocalAnd = new AndGate();
                    m_gLocalAnd.ConnectInput1(bwag.Input1[i]);
                    m_gLocalAnd.ConnectInput2(bwag.Input2[i]);
                    if(bwag.Input1[i].Value == bwag.Input2[i].Value && bwag.Input2[i].Value == 1)
                    {
                        if (m_gLocalAnd.Output.Value != 1 || bwag.m_gAnd[i].Output.Value != m_gLocalAnd.Output.Value)
                            return false;
                    }
                    else
                    {
                        if (m_gLocalAnd.Output.Value != 0)
                            return false;
                    }
                }
                // UNCOMMENT THIS LINES TO SEE THE DEBUG PRINT
                //System.Console.WriteLine("    Testing input1 " + " -> " + WStoString(bwag.Input1));
                //System.Console.WriteLine("    Testing input2 " + " -> " + WStoString(bwag.Input2));
                //System.Console.WriteLine("    Testing output " + " -> " + WStoString(bwag.Output));
                
                
            }
            return true;
        }

    }
}
