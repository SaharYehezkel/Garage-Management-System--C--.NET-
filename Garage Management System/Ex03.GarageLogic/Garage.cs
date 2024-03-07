using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleOwner> m_AllVehiclesInGarage;

        public Dictionary<string, VehicleOwner> AllVehiclesInGarage
        {
            get
            {
                return m_AllVehiclesInGarage;
            }
        }

        public Garage()
        {
            m_AllVehiclesInGarage = new Dictionary<string, VehicleOwner>();
        }

        public void AddNewVehicleToGarage(string i_LicensePlate, VehicleOwner i_Owner)
        {
            if (AllVehiclesInGarage.ContainsKey(i_LicensePlate))
            {
                throw new DuplicateWaitObjectException("Vehicle already exist in system.");
            }
            else
            {
                AllVehiclesInGarage.Add(i_LicensePlate, i_Owner);
            }
        }
    }
}