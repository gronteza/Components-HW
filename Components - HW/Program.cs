using Components;
using System;

namespace logic_gates
{
    class Program
    {
        static void Main(string[] args)
        {
            WireSet ws = new WireSet(9);
            WireSet ws2 = new WireSet(9);
            WireSet ws3 = new WireSet(9);
            OrGate or = new OrGate();

            XorGate xor = new XorGate();

            MultiBitAndGate mbag3 = new MultiBitAndGate(3);
            MultiBitAndGate mbag4 = new MultiBitAndGate(4);
            MultiBitAndGate mbag5 = new MultiBitAndGate(5);
            MultiBitAndGate mbag6 = new MultiBitAndGate(6);
            MultiBitAndGate mbag7 = new MultiBitAndGate(7);
            MultiBitAndGate mbag8 = new MultiBitAndGate(8);

            MultiBitOrGate mbog3 = new MultiBitOrGate(3);
            MultiBitOrGate mbog4 = new MultiBitOrGate(4);
            MultiBitOrGate mbog5 = new MultiBitOrGate(5);
            MultiBitOrGate mbog6 = new MultiBitOrGate(6);
            MultiBitOrGate mbog7 = new MultiBitOrGate(7);
            MultiBitOrGate mbog8 = new MultiBitOrGate(8);

            MuxGate mg = new MuxGate();
            Demux dmg = new Demux();

            BitwiseOrGate bwog0 = new BitwiseOrGate(0);
            BitwiseOrGate bwog1 = new BitwiseOrGate(1);
            BitwiseOrGate bwog2 = new BitwiseOrGate(2);
            BitwiseOrGate bwog3 = new BitwiseOrGate(3);
            BitwiseOrGate bwog4 = new BitwiseOrGate(4);
            BitwiseOrGate bwog5 = new BitwiseOrGate(5);
            BitwiseOrGate bwog6 = new BitwiseOrGate(6);
            BitwiseOrGate bwog7 = new BitwiseOrGate(7);

            BitwiseAndGate bwag2 = new BitwiseAndGate(2);
            BitwiseAndGate bwag3 = new BitwiseAndGate(3);
            BitwiseAndGate bwag4 = new BitwiseAndGate(4);

            BitwiseNotGate bwng2 = new BitwiseNotGate(2);
            BitwiseNotGate bwng3 = new BitwiseNotGate(3);
            BitwiseNotGate bwng4 = new BitwiseNotGate(4);

            BitwiseDemux bwdm2 = new BitwiseDemux(2);
            BitwiseDemux bwdm3 = new BitwiseDemux(3);
            BitwiseDemux bwdm4 = new BitwiseDemux(4);

            BitwiseMux bwmx2 = new BitwiseMux(2);
            BitwiseMux bwmx3 = new BitwiseMux(3);
            BitwiseMux bwmx4 = new BitwiseMux(4);

            BitwiseMultiwayMux bwmwm = new BitwiseMultiwayMux(3, 3);
            BitwiseMultiwayDemux bwmwdm = new BitwiseMultiwayDemux(3, 3);

            HalfAdder ha = new HalfAdder();
            FullAdder fa = new FullAdder();
            MultiBitAdder mba = new MultiBitAdder(4);
            ALU alu = new ALU(4);

            System.Console.WriteLine(or.TestGate().ToString());

            System.Console.WriteLine(xor.TestGate().ToString());

            System.Console.WriteLine(mbag3.TestGate().ToString());
            System.Console.WriteLine(mbag4.TestGate().ToString());
            System.Console.WriteLine(mbag5.TestGate().ToString());
            System.Console.WriteLine(mbag6.TestGate().ToString());
            System.Console.WriteLine(mbag7.TestGate().ToString());
            System.Console.WriteLine(mbag8.TestGate().ToString());

            System.Console.WriteLine(mbog3.TestGate().ToString());
            System.Console.WriteLine(mbog4.TestGate().ToString());
            System.Console.WriteLine(mbog5.TestGate().ToString());
            System.Console.WriteLine(mbog6.TestGate().ToString());
            System.Console.WriteLine(mbog7.TestGate().ToString());
            System.Console.WriteLine(mbog8.TestGate().ToString());

            System.Console.WriteLine(mg.TestGate().ToString());
            System.Console.WriteLine(dmg.TestGate().ToString());

            System.Console.WriteLine(bwag2.TestGate().ToString());
            System.Console.WriteLine(bwag3.TestGate().ToString());
            System.Console.WriteLine(bwag4.TestGate().ToString());

            System.Console.WriteLine(bwog0.TestGate().ToString());
            System.Console.WriteLine(bwog1.TestGate().ToString());
            System.Console.WriteLine(bwog2.TestGate().ToString());
            System.Console.WriteLine(bwog3.TestGate().ToString());
            System.Console.WriteLine(bwog4.TestGate().ToString());
            System.Console.WriteLine(bwog5.TestGate().ToString());
            System.Console.WriteLine(bwog6.TestGate().ToString());
            System.Console.WriteLine(bwog7.TestGate().ToString());

            ws.Set2sComplement(-5);
            System.Console.WriteLine(ws.Get2sComplement().ToString());
            int test = 0;
            int test2 = 0;
            for ( int i = 1; i < 50; i++)
            {
                ws2.SetValue(i);
                if (ws2.GetValue() != i)
                    test = 10;
            }
            for (int i = -34; i < 50; i++)
            {
                ws3.Set2sComplement(i);
                if (ws3.Get2sComplement() != i)
                    test2 = 10;
            }
            System.Console.WriteLine(test);
            System.Console.WriteLine(test2);

            System.Console.WriteLine(bwng2.TestGate().ToString());
            System.Console.WriteLine(bwng3.TestGate().ToString());
            System.Console.WriteLine(bwng4.TestGate().ToString());
            
            System.Console.WriteLine(bwdm2.TestGate().ToString());
            System.Console.WriteLine(bwdm3.TestGate().ToString());
            System.Console.WriteLine(bwdm4.TestGate().ToString());

            System.Console.WriteLine(bwmx2.TestGate().ToString());
            System.Console.WriteLine(bwmx3.TestGate().ToString());
            System.Console.WriteLine(bwmx4.TestGate().ToString());

            System.Console.WriteLine(bwmwm.TestGate().ToString());

            System.Console.WriteLine(bwmwdm.TestGate().ToString());

            System.Console.WriteLine(ha.TestGate().ToString());
            System.Console.WriteLine(fa.TestGate().ToString());
            System.Console.WriteLine(mba.TestGate().ToString());
            
            System.Console.WriteLine(alu.TestGate().ToString());

        }
    }
}
