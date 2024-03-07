using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;
        private readonly string r_ErrorMessage;

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return r_ErrorMessage;
            }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_ErrorMessage)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
            r_ErrorMessage = i_ErrorMessage;
        }
    }
}