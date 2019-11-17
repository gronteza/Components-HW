using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    //The specification can be found at https://b1391bd6-da3d-477d-8c01-38cdf774495a.filesusr.com/ugd/56440f_2e6113c60ec34ed0bc2035c9d1313066.pdf slides 48,49
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Output { get; private set; }

        //Control bit 
        public Wire ZeroX { get; private set; }
        public Wire ZeroY { get; private set; }
        public Wire NotX { get; private set; }
        public Wire NotY { get; private set; }
        public Wire F { get; private set; }
        public Wire NotOutput { get; private set; }

        //Bit outputs
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }


        //your code here
        private BitwiseMux muxZeroX;
        private BitwiseMux muxZeroY;
        private BitwiseMux muxNegX;
        private BitwiseMux muxNegY;
        private BitwiseMux muxFunc;
        private BitwiseMux muxNegOut;
        private MultiBitAdder mbAdder;
        private BitwiseAndGate bwAnd;
        private MultiBitOrGate mbOr;
        private BitwiseNotGate bwNot1;
        private BitwiseNotGate bwNot2;
        private BitwiseNotGate bwNot3;
        private WireSet zeroWS;
        private AndGate negAnd;
        private Wire notBit;
        private NotGate not1;



        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();            
            Zero = new Wire();
            

            //Create and connect all the internal components
            muxZeroX = new BitwiseMux(Size);
            muxZeroY = new BitwiseMux(Size);
            muxNegX = new BitwiseMux(Size);
            muxNegY = new BitwiseMux(Size);
            muxFunc = new BitwiseMux(Size);
            muxNegOut = new BitwiseMux(Size);
            mbAdder = new MultiBitAdder(Size);
            bwAnd = new BitwiseAndGate(Size);
            mbOr = new MultiBitOrGate(Size);
            bwNot1 = new BitwiseNotGate(Size);
            bwNot2 = new BitwiseNotGate(Size);
            bwNot3 = new BitwiseNotGate(Size);
            zeroWS = new WireSet(Size);
            negAnd = new AndGate();
            notBit = new Wire();
            not1 = new NotGate();

            // Zero X
            muxZeroX.Input1.ConnectInput(InputX);
            muxZeroX.Input2.ConnectInput(zeroWS);
            muxZeroX.ConnectControl(ZeroX);

            // Not X
            bwNot1.ConnectInput(muxZeroX.Output);
            muxNegX.Input1.ConnectInput(muxZeroX.Output);
            muxNegX.Input2.ConnectInput(bwNot1.Output);
            muxNegX.ConnectControl(NotX);
            
            // Zero Y
            muxZeroY.Input1.ConnectInput(InputY);
            muxZeroY.Input2.ConnectInput(zeroWS);
            muxZeroY.ConnectControl(ZeroY);

            // Not Y
            bwNot2.ConnectInput(muxZeroY.Output);
            muxNegY.Input1.ConnectInput(muxZeroY.Output);
            muxNegY.Input2.ConnectInput(bwNot2.Output);
            muxNegY.ConnectControl(NotY);

            // Func
            mbAdder.ConnectInput1(muxNegX.Output);
            mbAdder.ConnectInput2(muxNegY.Output);
            bwAnd.Input1.ConnectInput(muxNegX.Output);
            bwAnd.Input2.ConnectInput(muxNegY.Output);
            muxFunc.Input1.ConnectInput(bwAnd.Output);
            muxFunc.Input2.ConnectInput(mbAdder.Output);
            muxFunc.ConnectControl(F);
            
            // Not Output
            bwNot3.ConnectInput(muxFunc.Output);
            muxNegOut.Input1.ConnectInput(muxFunc.Output);
            muxNegOut.Input2.ConnectInput(bwNot3.Output);
            muxNegOut.ConnectControl(NotOutput);
        
            // Zero
            mbOr.ConnectInput(muxNegOut.Output);
            not1.ConnectInput(mbOr.Output);
            Zero.Value = not1.Output.Value;

            // Negative
            notBit.Value = 1;
            negAnd.Input1.ConnectInput(notBit);
            negAnd.Input2.ConnectInput(muxNegOut.Output[muxNegOut.Size -1]);

            // Output
            //Output = bwAnd.Output;
            Output = muxNegOut.Output;
        }

        public override bool TestGate()
        {
            ALU al;
            WireSet[] inputs = new WireSet[2];
            WireSet[] solution = new WireSet[18];
            WireSet[] all = new WireSet[6];

            for (int i=0;i<all.Length;i++)
                all[i] = new WireSet(18);
            
            
            WireSet wsTemp = new WireSet(18);
            WireSet wsTemp2 = new WireSet(Size);
            wsTemp.SetValue(240288);
            all[0].ConnectInput(wsTemp);
            wsTemp = new WireSet(18);
            wsTemp.SetValue(109481);
            all[1].ConnectInput(wsTemp);
            wsTemp = new WireSet(18);
            wsTemp.SetValue(251200);
            all[2].ConnectInput(wsTemp);
            wsTemp = new WireSet(18);
            wsTemp.SetValue(87493);
            all[3].ConnectInput(wsTemp);
            wsTemp = new WireSet(18);
            wsTemp.SetValue(231420);
            all[4].ConnectInput(wsTemp);
            wsTemp = new WireSet(18);
            wsTemp.SetValue(73613);
            all[5].ConnectInput(wsTemp);
            
            for (int i=0;i<Size;i++){
                for (int j=0;j<Size;j++){
                    
                    for (int f=0;f<solution.Length;f++)
                        solution[f] = new WireSet(Size);
            
                    inputs[0] = new WireSet(Size);
                    inputs[1] = new WireSet(Size);
                    inputs[0].SetValue(i);       
                    inputs[1].SetValue(j);       

                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(0);
                    solution[0].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(1);
                    solution[1].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.Set2sComplement(-1);
                    solution[2].ConnectInput(wsTemp2);
                    solution[3].ConnectInput(inputs[0]);
                    solution[4].ConnectInput(inputs[1]);
            
                    BitwiseNotGate bwng = new BitwiseNotGate(Size);
                    bwng.ConnectInput(inputs[0]);

                    solution[5].ConnectInput(bwng.Output);
                    bwng = new BitwiseNotGate(Size);
                    bwng.ConnectInput(inputs[1]);
                    solution[6].ConnectInput(bwng.Output);
                    wsTemp2 = new WireSet(Size);
            
                    wsTemp2.SetValue(inputs[0].GetValue() *-1);
                    solution[7].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[1].GetValue() *-1);
                    solution[8].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[0].GetValue() +1);
                    solution[9].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[1].GetValue() +1);
                    solution[10].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[0].GetValue() -1);
                    solution[11].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[1].GetValue() -1);
                    solution[12].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[0].GetValue() +inputs[1].GetValue());
                    solution[13].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[0].GetValue() -inputs[1].GetValue() );
                    solution[14].ConnectInput(wsTemp2);
                    wsTemp2 = new WireSet(Size);
                    wsTemp2.SetValue(inputs[1].GetValue() -inputs[0].GetValue());
                    solution[15].ConnectInput(wsTemp2);
            
                    BitwiseOrGate bwog = new BitwiseOrGate(Size);
                    BitwiseAndGate bwag = new BitwiseAndGate(Size);
                    bwog.ConnectInput1(inputs[0] );
                    bwog.ConnectInput2(inputs[1]);
                    bwag.ConnectInput1(inputs[0]);
                    bwag.ConnectInput2(inputs[1]);
                    solution[16].ConnectInput(bwag.Output);
                    solution[17].ConnectInput(bwog.Output);

                    for (int k=0;k<18;k++)
                    {
                        al = new ALU(Size);
                        al.InputX.ConnectInput(inputs[0]);
                            al.InputY.ConnectInput(inputs[1]);
                            al.ZeroX.Value = all[0][17-k].Value;
                            al.NotX.Value = all[1][17-k].Value;
                            al.ZeroY.Value = all[2][17-k].Value;
                            al.NotY.Value = all[3][17-k].Value;
                            al.F.Value = all[4][17-k].Value;
                            al.NotOutput.Value = all[5][17-k].Value;

                        if (al.Output.ToString() != solution[k].ToString())
                            return false;
                    }
                }               
            }
            return true;
        }
    }
}
