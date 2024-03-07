using System;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : FuelEngine
    {
        private const int k_AmountOfWheels = 2;
        private const float k_MaximumWheelAirPressure = 29f;
        private const float k_MaximumTankFuelCapacityInLiters = 5.8f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
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
                    throw new ArgumentException("Invalid input of driver license type.");
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

        public float FuelTankCapacity
        {
            get
            {
                return k_MaximumTankFuelCapacityInLiters;
            }
        }

        public static float MaximumFuelTankCapacity
        {
            get
            {
                return k_MaximumTankFuelCapacityInLiters;
            }
        }

        public FuelMotorcycle(eMotorcycleDriverLicenseType i_MotorcycleDriverLicenseType, int i_EngineCubicCapacity, 
            float i_AmountOfFuelLeft, float i_MaximumAmountOfFuel, string i_ModelName, string i_LicenseId,
            float i_EnergyLeftInPercentage, Wheel[] i_Wheels) 
            : base(k_FuelType, i_AmountOfFuelLeft, k_MaximumTankFuelCapacityInLiters, i_ModelName, i_LicenseId, (i_AmountOfFuelLeft / i_MaximumAmountOfFuel * 100), i_Wheels)
        {
            m_MotorcycleDriverLicenseType = i_MotorcycleDriverLicenseType;
            r_EngineCubicCapacity = i_EngineCubicCapacity;
        }
    }
}