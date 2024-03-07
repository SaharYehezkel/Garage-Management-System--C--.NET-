using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public class GarageConsoleInterface
    {
        private const string k_UserSelctionOptions = "Enter your selction:\n1. Add New Vehicle\n2. Show All Vehicles In Garage\n3. Change Vehicle Status\n4. Tire Inflation\n" +
            "5. Refuel Engine (Fuel engine only!)\n6. Recharge Electric Engine\n7. Load Vehicle Information\n8. Exit";

        private const string k_AllVehiclesSelection = "Choose vehicle type to add:\n1. Fuel Car\n2. Electric Car\n3. Fuel Motorcycle\n4. Electric Motorcycle\n5. Fuel Truck";

        private const string k_AllFuelTypesSelectionMessage = "Choose fuel type:\n1. Soler\n2. Octan95\n3. Octan96\n4. Octan98";

        private const string k_GarageStatusToPresentChooseMessage = "Choose which status to present:\n1. Under Repair\n2. Fixed\n3. Paid";

        private const string k_AllCarColorsSelectionMessage = "Choose car color:\n1. Blue\n2. White\n3. Red\n4. Yellow";

        private const string k_AllCarDoorsSelectionMessage = "Choose the amount of doors(2 to 5):\n2. Two Doors\n3. Three Doors\n4. Four Doors\n5. Five Doors";

        private const string k_AllMotorcycleDriverLicenseTypeSelection = "Enter motorcycle driver license type:\n1. A1\n2. A2\n3. AB\n4. B2";

        private static Garage s_Garage = new Garage();

        public void MainMenuUserSelection()
        {
            eUserSelection selection;

            Console.WriteLine("Welcome To the garage manager!");
            do
            {
                selection = (eUserSelection)getUserSelection(getFirstEnumValue<eUserSelection>(), getLastEnumValue<eUserSelection>(), k_UserSelctionOptions);
                handleUserSelection(selection);
            }
            while (selection != eUserSelection.Exit);
        }

        private void handleUserSelection(eUserSelection selection)
        {
            switch (selection)
            {
                case eUserSelection.AddNewVehicle:
                    addNewVehicleSelection();
                    break;
                case eUserSelection.ShowAllVehiclesInGarage:
                    showAllVehiclesInGarage();
                    break;
                case eUserSelection.ChangeVehicleStatus:
                    changeVehicleStatusInGarage();
                    break;
                case eUserSelection.TireInflation:
                    doTireInflationToMaximum();
                    break;
                case eUserSelection.RefuelFuelEngine:
                    refuelFuelVehicle();
                    break;
                case eUserSelection.RechargeElectricEngine:
                    rechargeElectricVehicle();
                    break;
                case eUserSelection.LoadVehicleInfo:
                    getFullInformationForVehicleInGarage();
                    break;
            }
        }

        private void getFullInformationForVehicleInGarage()
        {
            string licensePlate;
            VehicleOwner vehicleOwner;
            int wheelNumber = 1;
            Wheel[] wheelCollection;

            try
            {
                Console.WriteLine("Enter license plate id:");
                licensePlate = Console.ReadLine();
                if (!s_Garage.AllVehiclesInGarage.ContainsKey(licensePlate))
                {
                    throw new KeyNotFoundException("License plate id not found!");
                }

                vehicleOwner = s_Garage.AllVehiclesInGarage[licensePlate];
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Vehicle owner name: {0}", vehicleOwner.FullName);
                Console.WriteLine("Vehicle owner phone number: {0}", vehicleOwner.PhoneNumber);
                Console.WriteLine("Vehicle status: {0}", getStatusInString(vehicleOwner.Status));
                Console.WriteLine("Vehicle Type: {0}", getVehicleSubType(s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile));
                if (vehicleOwner.OwnedVechile is ElectricEngine)
                {
                    var vehicle = vehicleOwner.OwnedVechile as ElectricEngine;

                    Console.WriteLine("Vehicle current amount of batteries in percentage: {0}%", vehicle.EnergyLeftInPercentage);
                    Console.WriteLine("Vehicle current amount of batteries: {0}kWh", vehicle.KiloWattPerHourLeft);
                    Console.WriteLine("Vehicle maximum batteries capacity: {0} hours", vehicle.EngineMaximumHours);
                }
                else
                {
                    var vehicle = vehicleOwner.OwnedVechile as FuelEngine;
                    Console.WriteLine("Vehicle current amount of fuel in tank: {0} L", vehicle.AmountOfFuelLeft);
                    Console.WriteLine("Vehicle maximum fuel tank capacity: {0} L", vehicle.MaximumAmountOfFuel);
                    Console.WriteLine("Vehicle current amount of fuel in percentage: {0}%", vehicle.EnergyLeftInPercentage);
                }

                if (vehicleOwner.OwnedVechile is FuelCar fuelCar)
                {
                    Console.WriteLine("Vehicle color: {0}", fuelCar.CarColor);
                    Console.WriteLine("Amount of doors: {0}", fuelCar.AmountOfDoors);
                    Console.WriteLine("Fuel Type: {0}", fuelCar.eFuelType);
                }
                else if (vehicleOwner.OwnedVechile is ElectricCar electricCar)
                {
                    Console.WriteLine("Vehicle color: {0}", electricCar.CarColor);
                    Console.WriteLine("Amount of doors: {0}", electricCar.AmountOfDoors);
                }
                else if (vehicleOwner.OwnedVechile is FuelMotorcycle fuelMotorcycle)
                {
                    Console.WriteLine("Driver license type: {0}", fuelMotorcycle.MotorcycleDriverLicenseType);
                    Console.WriteLine("Engine cubic capacity: {0} cc", fuelMotorcycle.EngineCubicCapacity);
                    Console.WriteLine("Fuel Type: {0}", fuelMotorcycle.eFuelType);
                }
                else if (vehicleOwner.OwnedVechile is ElectricMotorcycle electricMotorcycle)
                {
                    Console.WriteLine("Driver license type: {0}", electricMotorcycle.MotorcycleDriverLicenseType);
                    Console.WriteLine("Engine cubic capacity: {0} cc", electricMotorcycle.EngineCubicCapacity);
                }
                else if (vehicleOwner.OwnedVechile is FuelTruck truck)
                {
                    Console.WriteLine("Cargo volume: {0} cubic meters", truck.CargoVolume);
                    Console.WriteLine("Carries dangerous material: {0}", truck.CarryDangerousMaterial ? "Yes" : "No");
                    Console.WriteLine("Fuel Type: {0}", truck.eFuelType);
                }

                Console.WriteLine("Vehicle Lincese Plate ID: {0}", vehicleOwner.OwnedVechile.LicenseId);
                Console.WriteLine("Vehicle model: {0}", vehicleOwner.OwnedVechile.ModelName);
                wheelCollection = vehicleOwner.OwnedVechile.WheelsCollection;
                foreach (Wheel wheel in wheelCollection)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("Wheel number {0}", wheelNumber++);
                    Console.WriteLine("Wheel producer: {0}", wheel.Producer);
                    Console.WriteLine("Wheel current air pressure: {0}", wheel.AirPressure);
                    Console.WriteLine("Wheel maximum air pressure: {0}", wheel.MaximumAirPressure);
                }

                Console.WriteLine("--------------------------------------");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string getStatusInString(eVehicleGarageStatus i_Status)
        {
            string status;

            if (i_Status == eVehicleGarageStatus.UnderRepair)
            {
                status = "Under Repair";
            }
            else if (i_Status == eVehicleGarageStatus.Fixed)
            {
                status = "Fixed";
            }
            else
            {
                status = "Paid";
            }

            return status;
        }

        private void rechargeElectricVehicle()
        {
            string licensePlate;
            float hoursToCharge;
            bool isValidInput = false;

            try
            {
                Console.WriteLine("Enter license plate id:");
                licensePlate = Console.ReadLine();
                if (s_Garage.AllVehiclesInGarage.ContainsKey(licensePlate))
                {
                    do
                    {
                        Console.WriteLine("Enter amount of hours to charge:");
                        if (float.TryParse(Console.ReadLine(), out hoursToCharge) && hoursToCharge >= 0f)
                        {
                            if (s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile is ElectricEngine)
                            {
                                (s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile as ElectricEngine).ChargeBatteries(hoursToCharge);
                                isValidInput = true;
                            }
                            else
                            {
                                throw new FormatException("Vehicle's engine type is not electric.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Amount must be number 0 or higher.");
                        }
                    } while (!isValidInput);
                }
                else
                {
                    throw new KeyNotFoundException("License plate id not found!");
                }
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine(exception.ErrorMessage);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void refuelFuelVehicle()
        {
            string licensePlate;
            eFuelType fuelType;
            float fuelAmount;
            bool isValidInput = false;

            try {
                Console.WriteLine("Enter license plate id:");
                licensePlate = Console.ReadLine();
                if (s_Garage.AllVehiclesInGarage.ContainsKey(licensePlate))
                {
                    fuelType = (eFuelType)getUserSelection(getFirstEnumValue<eFuelType>(), getLastEnumValue<eFuelType>(), k_AllFuelTypesSelectionMessage);
                    do
                    {
                        Console.WriteLine("Enter amount of fuel:");
                        if (float.TryParse(Console.ReadLine(), out fuelAmount) && fuelAmount >= 0f)
                        {
                            if (s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile is FuelEngine)
                            {
                                (s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile as FuelEngine).TankRefueling(fuelAmount, fuelType);
                                isValidInput = true;
                            }
                            else
                            {
                                throw new FormatException("Vehicle's engine type is not fuel.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Amount must be number 0 or higher.");
                        }
                    } while (!isValidInput);
                }
                else
                {
                    throw new KeyNotFoundException("License plate id not found!");
                }
            }
            catch (Exception exception) 
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void doTireInflationToMaximum()
        {
            string licensePlate;

            try
            {
                Console.WriteLine("For tire inflation to maximum please enter license plate:");
                licensePlate = Console.ReadLine();
                if (s_Garage.AllVehiclesInGarage.ContainsKey(licensePlate))
                {
                    foreach (Wheel wheel in s_Garage.AllVehiclesInGarage[licensePlate].OwnedVechile.WheelsCollection)
                    {
                        wheel.InflatingAWheel(wheel.MaximumAirPressure - wheel.AirPressure);
                    }

                    Console.WriteLine("All wheels air pressure at max!");
                }
                else
                {
                    throw new KeyNotFoundException("License plate not found.");
                }
            }
            catch (KeyNotFoundException exception)
            {
                Console.WriteLine("License plate not found...\n{0}", exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void changeVehicleStatusInGarage()
        {
            string licensePlate;
            eVehicleGarageStatus status;

            Console.WriteLine("Enter license plate:");
            licensePlate = Console.ReadLine();
            try
            {
                if (s_Garage.AllVehiclesInGarage.ContainsKey(licensePlate))
                {
                    status = (eVehicleGarageStatus)getUserSelection(getFirstEnumValue<eVehicleGarageStatus>(), getLastEnumValue<eVehicleGarageStatus>(), k_GarageStatusToPresentChooseMessage);
                    changeVehicleStatusInGarage(licensePlate, status);
                }
                else
                {
                    throw new KeyNotFoundException("License plate not found...");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void addNewVehicleSelection()
        {
            try
            {
                VehicleOwner vehicleOwner;
                Vehicle vehicle = null;
                eAllVehiclesType selection = (eAllVehiclesType)getUserSelection(getFirstEnumValue<eAllVehiclesType>(), getLastEnumValue<eAllVehiclesType>(), k_AllVehiclesSelection);

                switch (selection)
                {
                    case eAllVehiclesType.FuelMotorcycle:
                        {
                            vehicle = addVehicle(eAllVehiclesType.FuelMotorcycle);
                            break;
                        }

                    case eAllVehiclesType.ElectricMotorcycle:
                        {
                            vehicle = addVehicle(eAllVehiclesType.ElectricMotorcycle);
                            break;
                        }

                    case eAllVehiclesType.FuelCar:
                        {
                            vehicle = addVehicle(eAllVehiclesType.FuelCar);
                            break;
                        }

                    case eAllVehiclesType.ElectricCar:
                        {
                            vehicle = addVehicle(eAllVehiclesType.ElectricCar);
                            break;
                        }

                    case eAllVehiclesType.FuelTruck:
                        {
                            vehicle = addVehicle(eAllVehiclesType.FuelTruck);
                            break;

                        }
                }

                vehicleOwner = createVehicleOwner(vehicle);
                s_Garage.AddNewVehicleToGarage(vehicle.LicenseId ,vehicleOwner);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void showAllVehiclesInGarage()
        {
            string userSelection;
            bool isValidInput = false;
            eVehicleGarageStatus status;

            do
            {
                Console.WriteLine("Before presenting all vehicles, would you like to filter the list by status? (Y/N)");
                userSelection = Console.ReadLine();
                if (userSelection == "Y" || userSelection == "y")
                {
                    status = (eVehicleGarageStatus)getUserSelection(getFirstEnumValue<eVehicleGarageStatus>(), getLastEnumValue<eVehicleGarageStatus>(), k_GarageStatusToPresentChooseMessage);
                    presentVehiclesByStatus(status);
                    isValidInput = true;
                }
                else if (userSelection == "N" || userSelection == "n")
                {
                    presentAllVehicles();
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! enter Y to filter or N to present all.");
                }
            }
            while (!isValidInput);
        }

        private void presentVehiclesByStatus(eVehicleGarageStatus i_Status)
        {
            foreach (KeyValuePair<string, VehicleOwner> customer in s_Garage.AllVehiclesInGarage)
            {
                if (customer.Value.Status == i_Status)
                {
                    Console.WriteLine(customer.Value.OwnedVechile.LicenseId);
                }
            }
        }

        private void presentAllVehicles()
        {
            foreach (KeyValuePair<string, VehicleOwner> customer in s_Garage.AllVehiclesInGarage)
            {
                Console.WriteLine(customer.Value.OwnedVechile.LicenseId);
            }
        }

        private VehicleOwner createVehicleOwner(Vehicle i_Vehicle)
        {
            string fullName;
            string phoneNumber;

            Console.WriteLine("Enter vehicle owner name:");
            fullName = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            phoneNumber = Console.ReadLine();

            return new VehicleOwner(fullName, phoneNumber, i_Vehicle);
        }

        private string getVehicleSubType(Vehicle vehicle)
        {
            Type vehicleType;

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            vehicleType = vehicle.GetType();

            return vehicleType.Name;
        }

        private void changeVehicleStatusInGarage(string i_LicecsePlate, eVehicleGarageStatus i_NewStatus)
        {
            if (s_Garage.AllVehiclesInGarage.ContainsKey(i_LicecsePlate))
            {
                s_Garage.AllVehiclesInGarage[i_LicecsePlate].ChangeVehicleStatus(i_NewStatus);
                Console.WriteLine("Vehicle license plate - {0} - status changed to {1}.", i_LicecsePlate, i_NewStatus);
            }
            else
            {
                throw new KeyNotFoundException("License plate not found!");
            }
        }

        private Vehicle addVehicle(eAllVehiclesType i_VehicleType)
        {
            string licensePlateId;
            string model;
            float energyLeft;
            float maxEnergyCapacity;
            Wheel[] wheelsCollection;
            Vehicle vehicle = null;

            (licensePlateId, model, energyLeft, wheelsCollection) = getMainVehicleParams(i_VehicleType);
            switch (i_VehicleType)
            {
                case eAllVehiclesType.FuelTruck:
                    float cargoVolume;
                    bool carryDangerousMaterial;

                    (cargoVolume, carryDangerousMaterial, maxEnergyCapacity) = getFuelTruckParams(i_VehicleType);
                    vehicle = new FuelTruck(carryDangerousMaterial, cargoVolume, energyLeft, maxEnergyCapacity, model, licensePlateId, energyLeft / maxEnergyCapacity * 100, wheelsCollection);
                    break;

                case eAllVehiclesType.ElectricCar:
                case eAllVehiclesType.FuelCar:
                    eCarColorType carColor;
                    eAmountOfDoorsType amountOfDoors;

                    (maxEnergyCapacity, carColor, amountOfDoors) = getCarParams(i_VehicleType);
                    if (i_VehicleType == eAllVehiclesType.ElectricCar)
                    {
                        vehicle = new ElectricCar(carColor, amountOfDoors, energyLeft, maxEnergyCapacity, model, licensePlateId, wheelsCollection);
                    }
                    else
                    {
                        vehicle = new FuelCar(carColor, amountOfDoors, energyLeft, maxEnergyCapacity, model, licensePlateId, wheelsCollection);
                    }

                    break;

                case eAllVehiclesType.ElectricMotorcycle:
                case eAllVehiclesType.FuelMotorcycle:
                    eMotorcycleDriverLicenseType motorcycleDriverLicenseType;
                    int engineCubicCapacity;

                    (engineCubicCapacity, maxEnergyCapacity, motorcycleDriverLicenseType) = getMotorcycleParams(i_VehicleType);
                    if (i_VehicleType == eAllVehiclesType.ElectricMotorcycle)
                    {
                        vehicle = new ElectricMotorcycle(motorcycleDriverLicenseType, engineCubicCapacity, energyLeft, maxEnergyCapacity, model, licensePlateId, energyLeft, wheelsCollection);
                    }
                    else
                    {
                        vehicle = new FuelMotorcycle(motorcycleDriverLicenseType, engineCubicCapacity, energyLeft, maxEnergyCapacity, model, licensePlateId, energyLeft, wheelsCollection);
                    }

                    break;

                default:
                    {
                        throw new ArgumentException("Invalid vehicle type.");
                    }
            }

            if (vehicle == null)
            {
                throw new FormatException("Something failed in adding new vehicle and it not converted to any type of vehicle.");
            }

            return vehicle;
        }

        private int getFirstEnumValue<T>() where T : Enum
        {
            return Convert.ToInt32(Enum.GetValues(typeof(T)).Cast<T>().First());
        }

        private int getLastEnumValue<T>() where T : Enum
        {
            return Convert.ToInt32(Enum.GetValues(typeof(T)).Cast<T>().Last());
        }

        private int getUserSelection(int i_Min, int i_Max, string i_Message)
        {
            bool isSelectedSuccess = false;
            int selection;

            do
            {
                Console.WriteLine(i_Message);

                if (int.TryParse(Console.ReadLine(), out selection) && selection >= i_Min && selection <= i_Max)
                {
                    isSelectedSuccess = true;
                }
                else
                {
                    Console.WriteLine("Invalid selection! Please try again.");
                }
            }
            while (!isSelectedSuccess);

            return selection;
        }

        private (string, string, float, Wheel[]) getMainVehicleParams(eAllVehiclesType i_VehicleType)
        {
            string model;
            string licensePlate;
            float energyLeft = 0;
            Wheel[] wheelsCollection;

            licensePlate = getLicensePlateId();
            model = getVehicleModelName();
            energyLeft = getEnergyLeft(i_VehicleType);
            wheelsCollection = getUserInformationForWheelsCollection(i_VehicleType);

            return (licensePlate, model, energyLeft, wheelsCollection);
        }

        private (float, bool, float) getFuelTruckParams(eAllVehiclesType i_FuelTruck)
        {
            float cargoVolume;
            bool isCarryDangerousMaterial = false;
            bool isValidInput = false;
            float maxEnergy;
            string i_CarryDangerousMaterialMessage = "Does the current truck carry dangerous material?\n1. Yes\n2. No";

            do
            {
                Console.WriteLine("Enter cargo volume:");
                isValidInput = float.TryParse(Console.ReadLine(), out cargoVolume);
                if (cargoVolume < 0 || !isValidInput)
                {
                    Console.WriteLine("Invalid Input! Cargo volume value must be 0 or higher.");
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            do
            {
                Console.WriteLine(i_CarryDangerousMaterialMessage);
                string input = Console.ReadLine();
                if (input == "1")
                {
                    isCarryDangerousMaterial = true;
                    isValidInput = true;
                }
                else if (input == "2")
                {
                    isCarryDangerousMaterial = false;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Enter 1 for YES, 2 for NO.");
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            maxEnergy = getFullEnergyCapacityPerVehicleType(i_FuelTruck);

            return (cargoVolume, isCarryDangerousMaterial, maxEnergy);
        }

        private (float, eCarColorType, eAmountOfDoorsType) getCarParams(eAllVehiclesType i_Car)
        {
            float maxEnergyCapacity;
            eCarColorType carColor;
            eAmountOfDoorsType amountOfDoors;

            carColor = (eCarColorType)getUserSelection(getFirstEnumValue<eCarColorType>(), getLastEnumValue<eCarColorType>(), k_AllCarColorsSelectionMessage);
            amountOfDoors = (eAmountOfDoorsType)getUserSelection(getFirstEnumValue<eAmountOfDoorsType>(), getLastEnumValue<eAmountOfDoorsType>(), k_AllCarDoorsSelectionMessage);
            maxEnergyCapacity = getFullEnergyCapacityPerVehicleType(i_Car);

            return (maxEnergyCapacity, carColor, amountOfDoors);
        }

        private (int, float, eMotorcycleDriverLicenseType) getMotorcycleParams(eAllVehiclesType i_EngineType)
        {
            eMotorcycleDriverLicenseType motorcycleDriverLicenseType;
            int engineCubicCapacity;
            bool isValidInput = false;
            float maximumAmountOfFuel;

            do
            {
                Console.WriteLine("Enter engine cubic capacity:");
                isValidInput = int.TryParse(Console.ReadLine(), out engineCubicCapacity);
                if (!isValidInput || engineCubicCapacity < 1)
                {
                    Console.WriteLine("Error! EngineCC must be number higher then 0, try again.. ");
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            motorcycleDriverLicenseType = (eMotorcycleDriverLicenseType)getUserSelection(getFirstEnumValue<eMotorcycleDriverLicenseType>(),
                getLastEnumValue<eMotorcycleDriverLicenseType>(), k_AllMotorcycleDriverLicenseTypeSelection);
            maximumAmountOfFuel = getFullEnergyCapacityPerVehicleType(i_EngineType);

            return (engineCubicCapacity, maximumAmountOfFuel, motorcycleDriverLicenseType);
        }

        private string getLicensePlateId()
        {
            string licensePlateId = string.Empty;
            bool isValidInput = false;

            do
            {
                try
                {
                    Console.WriteLine("Enter License Plate Id:");
                    licensePlateId = Console.ReadLine();
                    if (string.IsNullOrEmpty(licensePlateId) || licensePlateId.Length < 0 || !licensePlateId.All(char.IsLetterOrDigit))
                    {
                        throw new ArgumentException("License plate must include characters or digits only and be with length of 1 at least.");
                    }

                    if (s_Garage.AllVehiclesInGarage.ContainsKey(licensePlateId))
                    {
                        throw new DuplicateWaitObjectException("Vehicle already exist!");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return licensePlateId;

        }

        private string getVehicleModelName()
        {
            string vehicleModel = string.Empty;
            bool isValidInput = false;

            do
            {
                try
                {
                    Console.WriteLine("Enter model:");
                    vehicleModel = Console.ReadLine();
                    if (string.IsNullOrEmpty(vehicleModel))
                    {
                        throw new ArgumentException("Vehicle model must include characters or digits only and be with length of 1 at least.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isValidInput);

            return vehicleModel;
        }

        private float getEnergyLeft(eAllVehiclesType i_VehicleType)
        {
            string userInput;
            float energyLeft = 0f;
            bool isValidInput = false;

            do
            {
                try
                {
                    Console.WriteLine("Enter amount of energy (Fuel - Liters | Electric - Hours) in vehicle right now:");
                    userInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(userInput))
                    {
                        throw new ArgumentException("Input cannot be empty.");
                    }
                    else if (!float.TryParse(userInput, out energyLeft))
                    {
                        throw new FormatException("Is not a number.");
                    }
                    else if (energyLeft > getMaximumEnergyAmountByType(i_VehicleType))
                    {
                        throw new ValueOutOfRangeException(1, getMaximumEnergyAmountByType(i_VehicleType), "Entered value of energy more then the capacity.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine("{0} range -> {1} - {2}", exception.ErrorMessage, exception.MinValue, exception.MaxValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isValidInput);

            return energyLeft;
        }

        private Wheel[] getUserInformationForWheelsCollection(eAllVehiclesType i_VehicleType)
        {
            string wheelProducer = string.Empty;
            float currentAirPressure = 0f;
            bool isValidInput = false;
            Wheel wheel;
            float maxAirPressure = getAirPressurePerVehicleType(i_VehicleType);

            do
            {
                try
                {
                    Console.WriteLine("Enter wheels producer:");
                    wheelProducer = Console.ReadLine();
                    if (string.IsNullOrEmpty(wheelProducer))
                    {
                        throw new ArgumentException("Wheel producer name must include characters or digits only and be with length of 1 at least.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine("{0} range -> {1} - {2}", exception.ErrorMessage, exception.MinValue, exception.MaxValue);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message); ;
                }
            }
            while (!isValidInput);

            isValidInput = false;
            do
            {
                try
                {
                    Console.WriteLine("Enter wheels air pressure:");
                    if (!float.TryParse(Console.ReadLine(), out currentAirPressure))
                    {
                        throw new FormatException("Input isn't anumber.");
                    }
                    if (currentAirPressure < 0f || currentAirPressure > maxAirPressure)
                    {
                        throw new ValueOutOfRangeException(0, maxAirPressure, "You cant enter air pressure more then the maximum.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine("{0} range -> {1} - {2}", exception.ErrorMessage, exception.MinValue, exception.MaxValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isValidInput);

            wheel = new Wheel(wheelProducer, currentAirPressure, maxAirPressure);

            return wheel.CreateWheelsCollection(wheel, getAmountOfWheelsPerVehicleType(i_VehicleType));
        }

        private float getMaximumEnergyAmountByType(eAllVehiclesType i_VehicleType)
        {
            float maximumEnergy = 0f;

            switch (i_VehicleType)
            {
                case eAllVehiclesType.FuelTruck:
                    maximumEnergy = FuelTruck.MaximumTankFuelCapacityInLiters;
                    break;
                case eAllVehiclesType.ElectricCar:
                    maximumEnergy = ElectricCar.BatteriesCapacityInHours;
                    break;
                case eAllVehiclesType.FuelCar:
                    maximumEnergy = FuelCar.MaximumTankFuelCapacityInLiters;
                    break;
                case eAllVehiclesType.ElectricMotorcycle:
                    maximumEnergy = ElectricMotorcycle.BatteriesCapacityInHours;
                    break;
                case eAllVehiclesType.FuelMotorcycle:
                    maximumEnergy = FuelMotorcycle.MaximumFuelTankCapacity;
                    break;
                default:
                    {
                        throw new ArgumentException("Invalid vehicle type.");
                    }
            }

            return maximumEnergy;
        }

        private float getAirPressurePerVehicleType(eAllVehiclesType i_VehicleType)
        {
            float airPressure = 0;

            switch (i_VehicleType)
            {
                case eAllVehiclesType.FuelMotorcycle:
                    {
                        airPressure = FuelMotorcycle.MaximumAirPressure;
                        break;
                    }

                case eAllVehiclesType.ElectricMotorcycle:
                    {
                        airPressure = ElectricMotorcycle.MaximumAirPressure;
                        break;
                    }

                case eAllVehiclesType.FuelCar:
                    {
                        airPressure = FuelCar.MaximumAirPressure;
                        break;
                    }

                case eAllVehiclesType.ElectricCar:
                    {
                        airPressure = ElectricCar.MaximumAirPressure;
                        break;
                    }

                case eAllVehiclesType.FuelTruck:
                    {
                        airPressure = FuelTruck.MaximumWheelAirPressure;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Error! Check the type you entered...");
                        break;
                    }
            }

            return airPressure;
        }

        private float getFullEnergyCapacityPerVehicleType(eAllVehiclesType i_VehicleType)
        {
            float fullEnergyCapacity = 0;

            switch (i_VehicleType)
            {
                case eAllVehiclesType.FuelMotorcycle:
                    {
                        fullEnergyCapacity = FuelMotorcycle.MaximumFuelTankCapacity;
                        break;
                    }

                case eAllVehiclesType.ElectricMotorcycle:
                    {
                        fullEnergyCapacity = ElectricMotorcycle.BatteriesCapacityInHours;
                        break;
                    }

                case eAllVehiclesType.FuelCar:
                    {
                        fullEnergyCapacity = FuelCar.MaximumTankFuelCapacityInLiters;
                        break;
                    }

                case eAllVehiclesType.ElectricCar:
                    {
                        fullEnergyCapacity = ElectricCar.BatteriesCapacityInHours;
                        break;
                    }

                case eAllVehiclesType.FuelTruck:
                    {
                        fullEnergyCapacity = FuelTruck.MaximumTankFuelCapacityInLiters;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Error! Check the type you entered...");
                        break;
                    }
            }

            return fullEnergyCapacity;
        }

        private int getAmountOfWheelsPerVehicleType(eAllVehiclesType i_VehicleType)
        {
            int amountOfWheels = 0;

            switch (i_VehicleType)
            {
                case eAllVehiclesType.FuelMotorcycle:
                    {
                        amountOfWheels = FuelMotorcycle.AmountOfWheels;
                        break;
                    }

                case eAllVehiclesType.ElectricMotorcycle:
                    {
                        amountOfWheels = ElectricMotorcycle.AmountOfWheels;
                        break;
                    }

                case eAllVehiclesType.FuelCar:
                    {
                        amountOfWheels = FuelCar.AmountOfWheels;
                        break;
                    }

                case eAllVehiclesType.ElectricCar:
                    {
                        amountOfWheels = ElectricCar.AmountOfWheels;
                        break;
                    }

                case eAllVehiclesType.FuelTruck:
                    {
                        amountOfWheels = FuelTruck.AmountOfWheels;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Error! Check the type you entered...");
                        break;
                    }
            }

            return amountOfWheels;
        }
    }
}