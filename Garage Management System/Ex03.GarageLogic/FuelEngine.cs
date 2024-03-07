using System;

namespace Ex03.GarageLogic
{
    public abstract class FuelEngine : Vehicle
    {
        private readonly eFuelType r_FuelType;
        private float m_AmountOfFuelLeft;
        private readonly float r_MaximumAmountOfFuel;

        public eFuelType eFuelType 
        { 
            get
            { 
                return r_FuelType;
            }
        }

        public float AmountOfFuelLeft
        {
            get
            {
                return m_AmountOfFuelLeft;
            }
        }

        public float MaximumAmountOfFuel
        {
            get
            {
                return r_MaximumAmountOfFuel;
            }
        }

        public FuelEngine(eFuelType i_FuelType, float i_AmountOfFuelLeft, float i_MaximumAmountOfFuel,
            string i_ModelName, string i_LicenseId, float i_EnergyLeftInPercentage, Wheel[] i_Wheels)
            : base(i_ModelName, i_LicenseId, i_EnergyLeftInPercentage, i_Wheels)
        {
            r_FuelType = i_FuelType;
            m_AmountOfFuelLeft = i_AmountOfFuelLeft;
            r_MaximumAmountOfFuel = i_MaximumAmountOfFuel;
        }

        public void TankRefueling(float  i_AmountOfFuel, eFuelType i_FuelType)
        {
            if (i_AmountOfFuel >= 0)
            {
                if (i_FuelType == r_FuelType)
                {
                    if (m_AmountOfFuelLeft + i_AmountOfFuel <= r_MaximumAmountOfFuel)
                    {
                        m_AmountOfFuelLeft += i_AmountOfFuel;
                        UpdateEngineEnergyLeftPercentage(m_AmountOfFuelLeft / r_MaximumAmountOfFuel * 100);
                        Console.WriteLine("Vehicle refueling succeeded.");
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0f, r_MaximumAmountOfFuel, "Value of amount of fuel is more then tank capacity!");
                    }
                }
                else
                {
                    throw new ArgumentException("You entered wrong type of fuel!");
                }
            }
        }
    }
}