using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        //your code here
        private MuxGate[] m_gMux;

        public BitwiseMux(int iSize)
            : base(iSize)
        {

            ControlInput = new Wire();
            //your code here
            m_gMux = new MuxGate[iSize];

            WireSet ws_resault = new WireSet(iSize);

            //your code here
            for (int i = 0; i < iSize; i++)
            {
                m_gMux[i] = new MuxGate();
                m_gMux[i].ConnectInput1(Input1[i]);
                m_gMux[i].ConnectInput2(Input2[i]);
                m_gMux[i].ConnectControl(ControlInput);

                ws_resault[i].ConnectInput(m_gMux[i].Output);

            }
            Output.ConnectInput(ws_resault);
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
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
            BitwiseMux bwmx;
            MuxGate m_gLocalMux;

            Wire w_local = new Wire();
            for (int l = 0; l < 2; l++)
            {
                w_local.Value = l;
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    bwmx = new BitwiseMux(Size);
                    bwmx.ConnectInput1(InitTestVariables(j));
                    bwmx.ConnectInput2(InitRandTestVar(Size));
                    bwmx.ConnectControl(w_local);

                    for (int i = 0; i < Size; i++)
                    {
                        m_gLocalMux = new MuxGate();
                        m_gLocalMux.ConnectInput1(bwmx.Input1[i]);
                        m_gLocalMux.ConnectInput2(bwmx.Input2[i]);
                        m_gLocalMux.ConnectControl(w_local);
                        if (bwmx.Input1[i].Value == bwmx.Input2[i].Value && bwmx.Input2[i].Value == 1)
                        {
                            if (m_gLocalMux.Output.Value != bwmx.Output[i].Value)
                                return false;
                        }
                    }
                    // UNCOMMENT THIS LINES TO SEE THE DEBUG PRINT
                    //System.Console.WriteLine("    Testing input1 " + " -> " + WStoString(bwmx.Input1));
                    //System.Console.WriteLine("    Testing input2 " + " -> " + WStoString(bwmx.Input2));
                    //System.Console.WriteLine("    Testing control" + " -> " + bwmx.ControlInput);
                    //System.Console.WriteLine("    Testing output " + " -> " + WStoString(bwmx.Output));
                }
            }
                return true;
        }
    }
}
