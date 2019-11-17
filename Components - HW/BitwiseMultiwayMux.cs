using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here
        private BitwiseMux[] bwmx;

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            
            for (int i = 0; i < Inputs.Length; i++)
                Inputs[i] = new WireSet(Size);

            bwmx = new BitwiseMux[(int)Math.Pow(2, cControlBits)-1];

            for (int i=0;i<bwmx.Length;i++)
                bwmx[i] = new BitwiseMux(iSize);

            //Level
            int j = 0;
            for (int i = 0; 2*i+2 < bwmx.Length; i++)
            {
                
                bwmx[i].ConnectInput1(bwmx[2 * i + 1].Output);
                bwmx[i].ConnectInput2(bwmx[2 * i + 2].Output);
                j = i+1;
            }
            
            for (int i = 0; i < Inputs.Length; i=i+2)
            {
                bwmx[j].ConnectInput1(Inputs[i]);
                bwmx[j].ConnectInput2(Inputs[i+1]);
                j++;
            }

            int k = 0;
            for (int i=0;i< bwmx.Length; i++)
            {
                if (i == (int)Math.Pow(2, k+1)-1)
                    k++;

                bwmx[i].ConnectControl(Control[Control.Size -k -1]);
            }

            Output.ConnectInput(bwmx[0].Output);

        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
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
                str += ws[ws.Size-1-i].Value.ToString();
            }
            return str;

        }
        public override bool TestGate()
        {
            BitwiseMultiwayMux m_localBwmx;
            
            for (int i = 0; i < Math.Pow(2,Control.Size); i++)
            {
                for( int j=0;j< Inputs.Length; j++)
                {
                    for (int k = 0; k < Math.Pow(2, Size); k++)
                    {
                        m_localBwmx = new BitwiseMultiwayMux(Size, Control.Size);
                        m_localBwmx.Control.ConnectInput(InitTestVariables(i,Control.Size));
                        m_localBwmx.Inputs[j].ConnectInput(InitTestVariables(k, Size));

                        //for (int p = 0; p < Inputs.Length; p++)
                        //    System.Console.WriteLine("    Testing input" + p + " " + " -> " + WStoString(m_localBwmx.Inputs[p]));

                        //System.Console.WriteLine("    Testing control " + "-> " + WStoString(m_localBwmx.Control));
                        //System.Console.WriteLine("    Testing output " + " -> " + WStoString(m_localBwmx.Output));

                        if (WStoString(m_localBwmx.Output) != WStoString(m_localBwmx.Inputs[
                            Convert.ToInt32(WStoString(m_localBwmx.Control),fromBase: 2)]))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
