using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            String str = Convert.ToString(iValue, 2);
            
            for (int i = 0; i < this.Size; i++)
                if(i <str.Length)
                    this[i].Value = int.Parse(str[str.Length -i-1].ToString());
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            String num ="";
            for (int i = 0; i < this.Size; i++)
                num += this[this.Size-i-1].Value;
            return Convert.ToInt32(num, 2);
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            int len = this.Size;

            int pos = len -1;
            int i = 0;

            while (i < len)
            {
                if ((iValue & (1 << i)) != 0)
                    this[this.Size - pos -1].Value = 1;
                else
                    this[this.Size - pos - 1].Value = 0;
                pos--;
                i++;
            }
        }
        

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (this[this.Size - 1].Value == 0)
                return Convert.ToInt32(this.ToString().Substring(1, this.ToString().Length - 2), fromBase: 2);
            
            string str = "";
            byte[] num = new byte[Size];
            int zeroCheck =0;
            for(int i = 0; i < this.Size; i++)
            {
                if (this[this.Size - i - 1].Value != 1)
                    str += 1;
                else
                {
                    str += 0;
                    zeroCheck++;
                }
            }

            return -1*(Convert.ToInt32(str, fromBase:2)+1);
        }


        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
