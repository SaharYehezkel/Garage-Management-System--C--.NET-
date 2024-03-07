using System;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private string m_FullName;
        private string m_PhoneNumber;
        private eVehicleGarageStatus m_Status;
        private Vehicle m_OwnedVehicle;
        public string FullName
        {
            get 
            { 
                return m_FullName;
            }
            set 
            {
                m_FullName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            { 
                m_PhoneNumber = value; 
            }
        }

        public eVehicleGarageStatus Status
        {
            get
            {
                return m_Status;
            }
        }

        public Vehicle OwnedVechile
        {
            get
            {
                return m_OwnedVehicle;
            }
        }

        public VehicleOwner(string i_FullName, string i_PhoneNumber, Vehicle i_OwnedVehicle)
        {
            m_Status = eVehicleGarageStatus.UnderRepair;
            m_FullName = i_FullName;
            m_PhoneNumber = i_PhoneNumber;
            m_OwnedVehicle = i_OwnedVehicle;
        }

        public void ChangeVehicleStatus(eVehicleGarageStatus i_Status)
        {
            {
                m_Status = i_Status;
            }
        }
    }
}
