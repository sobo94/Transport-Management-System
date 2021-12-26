# Transport-Management-System
---
The Transport Management System (TMS) is a WPF Application written in C# using MySQL Server.    

Group 02 - Software Quality Term Project. 

Team Member : Sohaib, Colby, Seaung Lee and Pariecheir
Video Demo  : [Youtube Link]

Software Technologies Used: 
WPF Application (.Net Framework) written in C#. 
DB: MySQL. 
SCRUM: Azure DevOp Boards.  
Software Diagramming: Visio, Lucid Chart.  
Wireframing: Figma, Adobe XD.
Misc: Unit Testing, Git, GitHub, DOxygen Documentation.


# Functionality
---
Support for 3 Different Roles Admin, Buyer and Planner. Application let's user pick their Job Role. They have to Authenticate with
the Passwords assigned based on functionality. User can make 3 attempts to login with the correct credential else they get locked out.


# Admin Functionality
---

Image: 

+ Access General Config
+ Review Log Files
+ Set Log File Directory
+ Update Rate Table 
+ Update Route Tables 
+ Update Carrier Tables
+ Create Data Backup
+ Set up Database Connection


# Buyer Functionality
---

Image: 

+ Connects to an external server which hosts a DB containing Clients orders.
+ New Client entries get generates every 10 mins. 
+ Buyer picks clients to service, by bringing external table entries into TMS Database
+ Assigns a Carrier City (Origin of our trip) to the Order Table.
+ Buyer can update the Order Status to be sent to the Planner.
+ Buyer can also review all the Completed Orders.
+ Buyer can generate Invoices for completed orders and stores it as an invoice File 


# Planner Functionality
---
Image:

- Planner based on the Order status view's the Order Table entries assigned to him.
- Planner selects an Order and based on the Route Information specified computes Order infomation.
- Planner computes the Total time, Total distance, Carrier Fee's and Clients Payment amount and Revenue of TMS generated on that order.
- Planner does a final review, Marks Order as Completed to be sent back to the buyer. 
- Planner finally updates database.
