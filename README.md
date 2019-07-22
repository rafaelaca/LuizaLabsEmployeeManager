# LuizaLabs Employee Manager


# DB Setup
This project was developed using MySQL Database.
1. Create a MySQL Database
2. Execute the script located at DB\scripts.sql to create all db objects
3. On the web.config of the project EmployeeManagerAPI, set the correct connection string. (Don't change the name "db_luizalabs_employee_manager")
4. Set the EmployeeManager as your Startup Project

# API Setup
1. Check if the API's url is set correctly at the EmployeeManager's web.config under 
appSettings -> key="EmployeeManagerApiUrl" value="http://localhost:60993/"

It must point to where EmployeeManagerAPI is published or being debugged.


# API Help Page
1. You can find the API help page at http://191.252.66.160/LuizaLabs/EmployeeManager/Home/Api


# Demo

You can check the project running at http://191.252.66.160/LuizaLabs/EmployeeManager/
