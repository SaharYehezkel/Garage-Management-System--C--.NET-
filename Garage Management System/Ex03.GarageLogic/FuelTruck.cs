using System;

namespace Ex03.GarageLogic
{
    public class FuelTruck : FuelEngine
    {
        private const int k_AmountOfWheels = 12;
        private const float k_MaximumWheelAirPressure = 28f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaximumTankFuelCapacityInLiters = 110f;
        private readonly bool r_CarryDangerousMaterials;
        private readonly float r_CargoVolume;

        public bool CarryDangerousMaterial
        {
            get
            {
                return r_CarryDangerousMaterials;
            }
        }

        public float CargoVolume
        {
            get
            {
                return r_CargoVolume;
            }
        }

        public static float MaximumWheelAirPressure
        {
            get
            {
                return k_MaximumWheelAirPressure;
            }
        }

        public static int AmountOfWheels
        {
            get
            {
                return k_AmountOfWheels;
            }
        }

        public static eFuelType FuelType
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

        public FuelTruck(bool i_CarryDangerousMaterials, float i_CargoVolume, float i_AmountOfFuelLeft, float i_MaximumAmountOfFuel,
            string i_ModelName, string i_LicenseId,float i_EnergyLeftInPercentage, Wheel[] i_Wheels)
            : base(k_FuelType, i_AmountOfFuelLeft, k_MaximumTankFuelCapacityInLiters, i_ModelName, i_LicenseId, i_EnergyLeftInPercentage, i_Wheels)
        {
            r_CargoVolume = i_CargoVolume;
            r_CarryDangerousMaterials = i_CarryDangerousMaterials;
        }
    }
}