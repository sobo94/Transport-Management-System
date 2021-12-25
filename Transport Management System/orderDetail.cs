using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2
{
    
    public class OrderDetail
        {

            private static readonly string[,] tmsCorridor = {
            {"Windsor", "191", "2.5", "END", "London" },
            {"London", "128", "1.75", "Windsor", "Hamilton"},
            {"Hamilton", "68", "1.25", "London", "Toronto"},
            {"Toronto", "60", "1.3", "Hamilton", "Oshawa"},
            {"Oshawa", "134", "1.65", "Toronto", "Belleville"},
            {"Belleville", "82", "1.2", "Oshawa", "Kingston"},
            {"Kingston", "196", "2.5", "Belleville", "Ottawa"},
            {"Ottawa", "N/A", "N/A", "Kingston", "END"},/** Transportation Corridor*/
        };

            private static readonly int ORIGIN_tmsCorridor = 0; /** Index of the origin city of a delivery in the transportation corridor*/
            private static readonly int DISTANCE_tmsCorridor = 1; /** Index of the KM column in the transportation corridor*/
            private static readonly int TIME_tmsCorridor = 2; /** Index of the time to travel between cities in the transportation corridor*/

            private static readonly int LOAD_TIME = 2;               /** Time to load a delivery truck*/
            private static readonly int UNLOAD_TIME = 2;             /** Time to unload a delivery truck*/
            private static readonly int INTERCITY_LOAD_TIME = 2;
            private static readonly int MAX_DRIVE_TIME_PER_DAY = 8; /** Max time a driver can drive in 24 hours*/
            private static readonly double MAX_TRANSPORT_TIME_PER_DAY = 12; /** Max time a driver can drive in 24 hours*/


            private static readonly int SURCHARGE = 150; /** Fee for when a delivery takes longer than 24 hours*/
            private static readonly double OSHT_FTL_markUp = 1.08; /** Mark up fee OSHT applies to carriers FTLRate to generate revenue*/
            private static readonly double OSHT_LTL_markUp = 1.05; /** Mark up fee OSHT applies to carriers FTLRate to generate revenue*/

            private static double totalTimeForDelivery = 0; /** Total Time a delivery takes including driving,load/unload*/

            private static double carrierRevenue = 0; /** revenue the carrier earns per delivery*/
            private static double OSHTRevenue = 0; /** revenue OSHT earns per delivery*/

            // INPUT from Contract Market Place is [Client Name, JobType, Origin, Destination, Van Type]
            // Order DB [Order ID, Employee ID, Status, Client Name, JobType, Origin, Destination, Van Type, ]
            // Function to Assign Carrier City 


            // Compute Distance 
            public int calcDistance(string startCity, string endCity)
            {

                int startCityRow = 0;
                int endCityRow = 0;
                int totalDistance = 0;

                // [1] Traverses the Corridor comparing Input Cities against the row of the Origin City.
                for (int i = 0; i < tmsCorridor.GetLength(0); i++)
                {
                    if (tmsCorridor[i, ORIGIN_tmsCorridor] == startCity)
                    {
                        startCityRow = i;
                    }
                    if (tmsCorridor[i, ORIGIN_tmsCorridor] == endCity)
                    {
                        endCityRow = i;
                    }
                }



                // [2] Start City is before End City in the TMSCorridor - Direction East       
                if (endCityRow > startCityRow)
                {
                    //[3] Iterate throught the TMS Corridor from Start city till end city adding up all distances
                    for (int x = startCityRow; x < endCityRow; x++)
                    {
                        totalDistance += Convert.ToInt32(tmsCorridor[x, DISTANCE_tmsCorridor]);
                    }
                }

                // [2] Start City is after End City in the TMSCorridor - Direction West 
                if (endCityRow < startCityRow)
                {
                    // [3] Iterate throught the TMS Corridor from End city back to start city adding up all distances
                    for (int x = endCityRow; x < startCityRow; x++)
                    {
                        totalDistance += Convert.ToInt32(tmsCorridor[x, DISTANCE_tmsCorridor]);
                    }
                }

                // [4] Check for same start and End City.  
                if (endCityRow == startCityRow)
                {
                    totalDistance = 0;
                }


                return totalDistance;
            }

            // Compute Time 
            public double calcTime(string startCity, string endCity, int Job_Type)
            {

                int startCityRow = 0;
                int endCityRow = 0;
                double drivingTime = 0;
                double extraTime = 0;
                double totalTime = 0;
                int citiesStoppedIn = 0;

                // [1] Traverses the Corridor comparing Input Cities against the row of the Origin City.
                for (int i = 0; i < tmsCorridor.GetLength(0); i++)
                {
                    if (tmsCorridor[i, ORIGIN_tmsCorridor] == startCity)
                    {
                        startCityRow = i;
                    }
                    if (tmsCorridor[i, ORIGIN_tmsCorridor] == endCity)
                    {
                        endCityRow = i;
                    }
                }



                // [2.1] Start City is before End City in the TMSCorridor - Direction East       
                if (endCityRow > startCityRow)
                {
                    // [3.1] - For LTL, Add Inter City Load time. We -1 to cities cause TMS Coridor array is 0 indexed.
                    if (Job_Type == 1)
                    {

                        citiesStoppedIn = endCityRow - startCityRow - 1;

                    }

                    if (Job_Type == 0)
                    {

                        citiesStoppedIn = 0;

                    }



                    //[3] Iterate throught the TMS Corridor from Start city till end city 
                    for (int x = startCityRow; x < endCityRow; x++)
                    {

                        // Add transit time for each city passed. 
                        drivingTime += Convert.ToDouble(tmsCorridor[x, TIME_tmsCorridor]);

                    }

                }



                // [2.2] Start City is before End City in the TMSCorridor - Direction West       
                if (endCityRow < startCityRow)
                {
                    // [3.1] - For LTL, Add Inter City Load time We + 1 to cities cause TMS Coridor array is 0 indexed.
                    if (Job_Type == 1)
                    {

                        citiesStoppedIn = startCityRow - endCityRow + 1;

                    }

                    if (Job_Type == 0)
                    {

                        citiesStoppedIn = 0;

                    }


                    //[3] Iterate throught the TMS Corridor from Start city till end city 
                    for (int x = endCityRow; x < startCityRow; x++)
                    {

                        // Add transit time for each city passed. 
                        drivingTime += Convert.ToDouble(tmsCorridor[x, TIME_tmsCorridor]);



                    }

                }



                // Compute Time
                extraTime = LOAD_TIME + (citiesStoppedIn * INTERCITY_LOAD_TIME) + UNLOAD_TIME;
                totalTime = drivingTime + extraTime;



                // [4] Check for same start and End City.  
                if (endCityRow == startCityRow)
                {
                    totalTime = 0;
                }


                return totalTime;

            }


            // [D] - IF WH LTL then Error
            public int CalcCarrierRevenue(int totalKMs, double totalTime, int Job_Type, string carrier, int vanType)
            {
                //driver can drive a max of 8 hours a day
                //driver can work a max of 12 hours in 1 day (this means the driver could load the truck, drive the maximum amount allowable in a day then unload the truck)

                //150$ added each day after the first day of the delivery
                //8% mark up on FTLRate
                double ratePerKM = 0;
                carrierRevenue = 0;                                                 //reset data member just in case
                int trip = 0;



                if (carrier == "Planet Express")
                {
                    if (Job_Type == 0)
                    {

                        ratePerKM = 5.21;              // FTL RATE

                    }
                    if (Job_Type == 1)
                    {
                        ratePerKM = 0.3821;         // LTL RATE
                    }

                    if (vanType == 1)               // With Ref
                    {
                        ratePerKM = ratePerKM * 1.08;
                    }
                }
                if (carrier == "Schooners")
                {
                    if (Job_Type == 0)
                    {

                        ratePerKM = 5.05;        // FTL RATE

                    }
                    if (Job_Type == 1)
                    {
                        ratePerKM = 0.3434;      // LTL RATE
                    }

                    if (vanType == 1)           // With Ref
                    {
                        ratePerKM = ratePerKM * 1.07;
                    }
                }
                if (carrier == "Tillman Transport")
                {
                    if (Job_Type == 0)
                    {

                        ratePerKM = 5.11; // FTL RATE

                    }
                    if (Job_Type == 1)
                    {
                        ratePerKM = 0.3012; // LTL RATE
                    }

                    if (vanType == 1) // With Ref
                    {
                        ratePerKM = ratePerKM * 1.09;
                    }
                }
                if (carrier == "We Haul")
                {
                    if (Job_Type == 0)
                    {

                        ratePerKM = 5.2;       // FTL RATE

                    }
                    if (Job_Type == 1)
                    {
                        ratePerKM = 0;           // LTL RATE - ERROR HERE
                    }

                    if (vanType == 1)           // With Ref
                    {
                        ratePerKM = ratePerKM * 1.065;
                    }
                }

                // Calculate revenue per KM for FTL OR LTL
                carrierRevenue = totalKMs * ratePerKM;






                // Calculate Surcharge per extra day
                trip = (int)(totalTime / MAX_TRANSPORT_TIME_PER_DAY);
                int extraDays = trip;
                Console.WriteLine("Trip Calculated : {0}", trip);


                if (totalTime > MAX_TRANSPORT_TIME_PER_DAY)
                {
                    carrierRevenue = carrierRevenue + extraDays * SURCHARGE;
                }

                return (int)carrierRevenue;
            }

            public int CalcOSHTRevenue(double CarrierRevenue, int Job_Type)
            {

                if (Job_Type == 0)
                {
                    // FTL 
                    OSHTRevenue = CarrierRevenue * OSHT_FTL_markUp;


                }

                if (Job_Type == 1)
                {
                    // LTL 
                    OSHTRevenue = CarrierRevenue * OSHT_LTL_markUp;

                }
                return (int)OSHTRevenue;

            }
        }


}
