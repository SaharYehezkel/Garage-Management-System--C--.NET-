using System;

namespace Ex03.GarageLogic
{
    public abstract class ElectricEngine : Vehicle
    {
        private float m_KiloWattPerHourLeft;
        private readonly float r_EngineMaximumHours;

        public float KiloWattPerHourLeft
        {
            get
            {
                return m_KiloWattPerHourLeft;
            }
        }

        public float EngineMaximumHours
        {
            get
            {
                return r_EngineMaximumHours;
            }
        }

        public ElectricEngine(float i_KiloWattPerHourLeft, float i_EngineMaximumHours, 
            string i_ModelName, string i_LicenseId, float i_EnergyLeftInPercentage, Wheel[] i_Wheels)
            : base(i_ModelName, i_LicenseId, i_EnergyLeftInPercentage, i_Wheels)
        {
            m_KiloWattPerHourLeft = i_KiloWattPerHourLeft;
            r_EngineMaximumHours = i_EngineMaximumHours;
        }

        public void ChargeBatteries(float i_EngineHoursCapacityToCharge)
        {
            if (m_KiloWattPerHourLeft + i_EngineHoursCapacityToCharge <= r_EngineMaximumHours && i_EngineHoursCapacityToCharge >= 0)
            {
                m_KiloWattPerHourLeft += i_EngineHoursCapacityToCharge;
                UpdateEngineEnergyLeftPercentage(m_KiloWattPerHourLeft / r_EngineMaximumHours * 100);
                Console.WriteLine("Vehicle batteries recharge succeeded.");
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_EngineMaximumHours, "Value of hours to charge is more then batteries capacity!");
            }
        }
    }
}