using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        //your code here
        private BitwiseDemux[] bwdmx;

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            //your code here

            bwdmx = new BitwiseDemux[(int)Math.Pow(2, cControlBits) - 1];

            for (int i = 0; i < bwdmx.Length; i++)
                bwdmx[i] = new BitwiseDemux(iSize);

            bwdmx[0].ConnectInput(Input);
            
            //Level
            int j = 0;
            for (int i = 0; 2 * i + 2 < bwdmx.Length; i++)
            {
                bwdmx[2 * i + 1].ConnectInput(bwdmx[i].Output1);
                bwdmx[2 * i + 2].ConnectInput(bwdmx[i].Output2);
                j = i + 1;
            }

            for (int i = 0; i < Outputs.Length; i = i + 2)
            {
                Outputs[i].ConnectInput(bwdmx[j].Output1);
                Outputs[i+1].ConnectInput(bwdmx[j].Output2);
                j++;
            }

            int k = 0;
            for (int i = 0; i < bwdmx.Length; i++)
            {
                if (i == (int)Math.Pow(2, k + 1) - 1)
                    k++;

                bwdmx[i].ConnectControl(Control[Control.Size - k - 1]);
            }
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }

        private WireSet InitTestVariables(int numToBin, int size)
        {
            // Init the wireset for the test 
            String strBinary = Convert.ToString(numToBin, 2);
            WireSet wsTest = new WireSet(size);
            Wire wireToConnect = new Wire();
            strBinary = strBinary.PadLeft(size, '0');

            for (int i = 0; i < wsTest.Size; i++)
                wsTest[i].Value = strBinary.ToCharArray()[wsTest.Size - i - 1] - '0';
            return wsTest;
        }
        private String WStoString(WireSet ws)
        {
            String str = "";

            for (int i = 0; i < ws.Size; i++)
            {
                str += ws[ws.Size - 1 - i].Value.ToString();
            }
            return str;

        }
        public override bool TestGate()
        {
            BitwiseMultiwayDemux m_localBwdmx;

            for (int i = 0; i < Math.Pow(2, Control.Size); i++)
            {
                for (int j = 0; j < Outputs.Length; j++)
                {
                    for (int k = 0; k < Math.Pow(2, Size); k++)
                    {
                        m_localBwdmx = new BitwiseMultiwayDemux(Size, Control.Size);
                        m_localBwdmx.Control.ConnectInput(InitTestVariables(i, Control.Size));
                        m_localBwdmx.Input.ConnectInput(InitTestVariables(k, Size));

                        //System.Console.WriteLine("    Testing input " + " -> " + WStoString(m_localBwdmx.Input));
                        //System.Console.WriteLine("    Testing control " + "-> " + WStoString(m_localBwdmx.Control));
                        //for (int p = 0; p < Outputs.Length; p++)
                        //    System.Console.WriteLine("    Testing output" + p + " " + " -> " + WStoString(m_localBwdmx.Outputs[p]));


                        if (WStoString(m_localBwdmx.Input) != WStoString(m_localBwdmx.Outputs[
                            Convert.ToInt32(WStoString(m_localBwdmx.Control), fromBase: 2)]))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
