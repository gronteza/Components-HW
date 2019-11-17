using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A mux has 2 inputs. There is a single output and a control bit, selecting which of the 2 inpust should be directed to the output.
    class MuxGate : TwoInputGate
    {
        public Wire ControlInput { get; private set; }
        //your code here
        private AndGate m_gAnd1;
        private AndGate m_gAnd2;
        private AndGate m_gAnd3;
        private MultiBitOrGate m_gmOr;
        private NotGate m_gNot;
        private WireSet m_ws;

        public MuxGate()
        {
            ControlInput = new Wire();

            //Init the variables
            m_gAnd1 = new AndGate();
            m_gAnd2 = new AndGate();
            m_gAnd3 = new AndGate();
            m_gNot = new NotGate();
            m_gmOr = new MultiBitOrGate(3);
            m_ws = new WireSet(3);

            //Connect the wires
            m_gNot.ConnectInput(ControlInput);
            m_gAnd1.ConnectInput1(Input1);
            m_gAnd1.ConnectInput2(Input2);
            m_gAnd2.ConnectInput1(Input2);
            m_gAnd2.ConnectInput2(ControlInput);
            m_gAnd3.ConnectInput1(Input1);
            m_gAnd3.ConnectInput2(m_gNot.Output);
            m_ws[0].ConnectInput(m_gAnd1.Output);
            m_ws[1].ConnectInput(m_gAnd2.Output);
            m_ws[2].ConnectInput(m_gAnd3.Output);
            m_gmOr.ConnectInput(m_ws);
            Output = m_gmOr.Output;

        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }


        public override string ToString()
        {
            return "Mux " + Input1.Value + "," + Input2.Value + ",C" + ControlInput.Value + " -> " + Output.Value;
        }



        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }
}
