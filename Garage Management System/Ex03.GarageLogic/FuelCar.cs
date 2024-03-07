using System;

namespace Ex03.GarageLogic
{
    public class FuelCar : FuelEngine
    {
        private const int k_AmountOfWheels = 5;
        private const float k_MaximumWheelAirPressure = 30f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaximumTankFuelCapacityInLiters = 58f;
        private eCarColorType m_CarColor;
        private readonly eAmountOfDoorsType r_AmountOfDoors;

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

        public eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public static float MaximumTankFuelCapacityInLiters
        {
            get
            {
                return k_MaximumTankFuelCapacityInLiters;
            }
        }

        public eCarColorType CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                if (Enum.IsDefined(typeof(eCarColorType), value))
                {
                    m_CarColor = value;
                }
                else
                {
                    throw new ArgumentException("You entered wrong color to car color!");
                }
            }
        }

        public eAmountOfDoorsType AmountOfDoors
        {
            get
            {
                return r_AmountOfDoors;
            }
        }

        public FuelCar(eCarColorType i_CarColor, eAmountOfDoorsType i_AmountOfDoors, float i_AmountOfFuelLeft, float i_MaximumAmountOfFuel, string i_ModelName, string i_LicenseId, Wheel[] i_Wheels)
            : base(k_FuelType, i_AmountOfFuelLeft, k_MaximumTankFuelCapacityInLiters, i_ModelName, i_LicenseId, (i_AmountOfFuelLeft / i_MaximumAmountOfFuel * 100), i_Wheels)
        {
            m_CarColor = i_CarColor;
            r_AmountOfDoors = i_AmountOfDoors;
        }
    }
}