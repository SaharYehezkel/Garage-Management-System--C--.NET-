using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Producer;
        private float m_AirPressure;
        private readonly float r_MaximumAirPressure;

        public string Producer
        {
            get 
            {
                return m_Producer; 
            }
            set
            {
                m_Producer = value;
            }
        }

        public float AirPressure
        {
            get
            { 
                return m_AirPressure; 
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return r_MaximumAirPressure;
            }
        }

        public Wheel(string i_Producer, float i_AirPressure, float i_MaximumAirPressure)
        {
            m_Producer = i_Producer;
            m_AirPressure = i_AirPressure;
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public Wheel(Wheel i_Wheel)
        {
            m_Producer = i_Wheel.Producer;
            m_AirPressure = i_Wheel.AirPressure;
            r_MaximumAirPressure = i_Wheel.MaximumAirPressure;
        }

        public void InflatingAWheel(float i_AirToAdd)
        {
            try
            {
                if (m_AirPressure + i_AirToAdd <= r_MaximumAirPressure)
                {
                    m_AirPressure += i_AirToAdd;
                }
                else
                {
                    throw new Exception("The system has detected that you are about to exceed the maximum amount of air pressure and stops at maximum.");
                }
            }
            catch (Exception exception)
            {
                m_AirPressure = r_MaximumAirPressure;
                Console.WriteLine(exception.Message);
            }
        }

        public Wheel[] CreateWheelsCollection(Wheel i_Prototype, int i_AmountOfWheels)
        {
            Wheel[] wheels = new Wheel[i_AmountOfWheels];

            for (int i = 0; i < i_AmountOfWheels; i++)
            {
                wheels[i] = new Wheel(i_Prototype);
            }

            return wheels;
        }
    }
}