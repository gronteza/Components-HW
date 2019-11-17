using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here
        private Demux[] m_gDemux;

        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);

            //your code here

            m_gDemux = new Demux[iSize];

            WireSet ws_resault1 = new WireSet(iSize);
            WireSet ws_resault2 = new WireSet(iSize);

            //your code here
            for (int i = 0; i < iSize; i++)
            {
                m_gDemux[i] = new Demux();
                m_gDemux[i].ConnectInput(Input[i]);
                m_gDemux[i].ConnectControl(Control);

                ws_resault1[i].ConnectInput(m_gDemux[i].Output1);
                ws_resault2[i].ConnectInput(m_gDemux[i].Output2);

            }
            Output1.ConnectInput(ws_resault1);
            Output2.ConnectInput(ws_resault2);
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
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
            BitwiseDemux bwdm;
            Demux m_gLocalDemux;
            Wire w_local = new Wire();
            for (int l = 0; l < 2; l++)
            {
                w_local.Value = l;
                
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    bwdm = new BitwiseDemux(Size);
                    bwdm.Input.ConnectInput(InitTestVariables(j));
                    bwdm.ConnectControl(w_local);

                    for (int i = 0; i < Size; i++)
                    {
                        m_gLocalDemux = new Demux();
                        m_gLocalDemux.ConnectInput(bwdm.Input[i]);
                        m_gLocalDemux.ConnectControl(bwdm.Control);
                        if (bwdm.Output1[i].Value != m_gLocalDemux.Output1.Value || bwdm.Output2[i].Value != m_gLocalDemux.Output2.Value)
                        {
                            return false;
                        }
                    }
                    //// UNCOMMENT THIS LINES TO SEE THE DEBUG PRINT
                    //System.Console.WriteLine("    Testing input " + " -> " + WStoString(bwdm.Input));
                    //System.Console.WriteLine("    Testing control " + " -> " + bwdm.Control);
                    //System.Console.WriteLine("    Testing output1 " + " -> " + WStoString(bwdm.Output1));
                    //System.Console.WriteLine("    Testing output2 " + " -> " + WStoString(bwdm.Output2));
                }
            }
                return true;
        }

    }
}
