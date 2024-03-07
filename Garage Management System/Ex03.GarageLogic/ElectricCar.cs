using System;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricEngine
    {
        private const int k_AmountOfWheels = 5;
        private const float k_MaximumWheelAirPressure = 30f;
        private const float k_BatteriesCapacityInHours = 4.8f;
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

        public static float BatteriesCapacityInHours
        {
            get
            {
                return k_BatteriesCapacityInHours;
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
                    throw new ArgumentException("You entered wrong input to car color.");
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

        public ElectricCar(eCarColorType i_CarColor, eAmountOfDoorsType i_AmountOfDoors, float i_AmountOfHoursLeft, float i_MaximumAmountOfHours, string i_ModelName, string i_LicenseId, Wheel[] i_Wheels)
            : base(i_AmountOfHoursLeft, k_BatteriesCapacityInHours, i_ModelName, i_LicenseId, (i_AmountOfHoursLeft / i_MaximumAmountOfHours * 100), i_Wheels)
        {
            m_CarColor = i_CarColor;
            r_AmountOfDoors = i_AmountOfDoors;
        }
    }
}