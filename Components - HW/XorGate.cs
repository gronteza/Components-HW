using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{

    //This gate implements the xor operation. To implement it, follow the example in the And gate.
    class XorGate : TwoInputGate
    {
        //your code here
        private NotGate m_gNot1;
        private NotGate m_gNot2;
        private NAndGate m_gNand1;
        private NAndGate m_gNand2;
        private NAndGate m_gNand3;
        public XorGate()
        {
            //init the variables
            m_gNand1 = new NAndGate();
            m_gNand2 = new NAndGate();
            m_gNand3 = new NAndGate();
            m_gNot1 = new NotGate();
            m_gNot2 = new NotGate();
            
            //wire the gates
            m_gNand1.ConnectInput1(m_gNot1.Output);
            m_gNand1.ConnectInput2(Input2);
        
            m_gNand2.ConnectInput1(Input1);
            m_gNand2.ConnectInput2(m_gNot2.Output);

            m_gNand3.ConnectInput1(m_gNand1.Output);
            m_gNand3.ConnectInput2(m_gNand2.Output);
            
            //set the inputs and the output of the OR gate
            Output = m_gNand3.Output;
            m_gNot1.Input.ConnectInput(Input1);
            m_gNot2.Input.ConnectInput(Input2);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(xor)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        //this method is used to test the gate. 
        //we simply check whether the truth table is properly implemented.
        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
             if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
