using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricEngine
    {
        private const int k_AmountOfWheels = 2;
        private const float k_MaximumWheelAirPressure = 29f;
        private const float k_BatteriesCapacityInHours = 2.8f;
        private eMotorcycleDriverLicenseType m_MotorcycleDriverLicenseType;
        private readonly int r_EngineCubicCapacity;

        public eMotorcycleDriverLicenseType MotorcycleDriverLicenseType
        {
            get
            {
                return m_MotorcycleDriverLicenseType;
            }
            set
            {
                if (Enum.IsDefined(typeof(eMotorcycleDriverLicenseType), value))
                {
                    m_MotorcycleDriverLicenseType = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(1, 4, "You entered invalid driver license type.");
                }
            }
        }

        public int EngineCubicCapacity
        {
            get
            {
                return r_EngineCubicCapacity;
            }
        }

        public static int AmountOfWheels
        {
            get
            {
                return k_AmountOfWheels;
            }
        }

        public static float MaximumAirPressure
        {
            get
            {
                return k_MaximumWheelAirPressure;
            }
        }

        public static float BatteriesCapacityInHours
        {
            get
            {
                return k_BatteriesCapacityInHours;
            }
        }

        public ElectricMotorcycle(eMotorcycleDriverLicenseType i_MotorcycleDriverLicenseType, int i_EngineCubicCapacity,
            float i_AmountOfHoursLeft, float i_MaximumAmountOfHours, string i_ModelName, string i_LicenseId,
            float i_EnergyLeftInPercentage, Wheel[] i_Wheels)
            : base(i_AmountOfHoursLeft, k_BatteriesCapacityInHours, i_ModelName, i_LicenseId, i_EnergyLeftInPercentage, i_Wheels)
        {
            m_MotorcycleDriverLicenseType = i_MotorcycleDriverLicenseType;
            r_EngineCubicCapacity = i_EngineCubicCapacity;
        }


    }
}