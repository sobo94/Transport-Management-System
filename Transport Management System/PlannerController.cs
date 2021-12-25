using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2
{
    public static class PlannerController

    // ======================================
    //             PUBLIC
    // ======================================

    {
        public static string 
        public static string 
        public static string 
      

        // Logger 4.5.2.1.2  -------- write with append / read 
        public static void addLog(string logMsg)
        {
            DateTime tsmTime = DateTime.Now;
            Record += $"[ {tsmTime} ] - {logMsg} \n";

            // append to a file
        }

        public static void readLog(string logMsg)
        {
            // it will display on the admin review log page
        }

         
        // this method gets the order from buyer
        // sorts and stores it in orders database
        //4.5.2.3.1
       static void getOrder()
        {
         
        }

        //this method lets the planner to choose  a carrier
        //also must be able to add trip if capacity is reached for a carrier
        //4.5.2.3.2 
        static void selectCarrier() 
         {
           
         }

        static void incrementData()
        {
            //4.5.2.3.4
        }

        // this method finds active orders and marks them in order to check their status
        // 4.5.2.3.5
        static void markForFollowUp()
        {
            
        }
        

        // creates query of active orders from order database
        static void summarizeActiceOrders()
        {
            
        }

        // creates report and sends it to buyer all-time or past 2 weeks.
        // 4.5.2.3.8 
        static void genarateReport()
        {

        }
    

    }
}