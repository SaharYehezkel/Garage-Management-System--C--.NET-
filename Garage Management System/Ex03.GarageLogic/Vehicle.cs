using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicensePlateId;
        private float m_EngineEnergyLeftInPercentage;
        private Wheel[] m_WheelsCollection;

        public string ModelName 
        { 
            get 
            { 
                return r_ModelName;
            } 
        }

        public string LicenseId
        {
            get
            { 
                return r_LicensePlateId; 
            }
        }

        public float EnergyLeftInPercentage
        {
            get
            {
                return m_EngineEnergyLeftInPercentage;
            }
            set
            {
                // Calculate the engergy left in percentage by (currentCapacity / totalCapacity * 100)
            }
        }

        public Wheel[] WheelsCollection
        {
            get
            {
                 return m_WheelsCollection;
            }
        }

        public Vehicle(string i_ModelName, string i_LicensePlateId, float i_EnergyLeftInPercentage, Wheel[] i_Wheels)
        {
            r_ModelName = i_ModelName;
            r_LicensePlateId = i_LicensePlateId;
            m_EngineEnergyLeftInPercentage = i_EnergyLeftInPercentage;
            m_WheelsCollection = i_Wheels;
        }

        public void UpdateEngineEnergyLeftPercentage(float i_EnergyLeftInPercentage)
        {
            m_EngineEnergyLeftInPercentage = i_EnergyLeftInPercentage;
        }
    }
}