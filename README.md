Project Name: ParseFlatFile

Intro: Parsing a big flat file using c# console application and Entity Framework 6

Description:
  This is a big flat text file where there is NO DELIMITER appart from spaces. The file contains a no of PROJECTS data and each PROJECT contains list of INFORMATIONS/DETAILS.
  In our Database there is two table, one is for PROJECTS and another is for PROJECT INFORMATION/DETAILS.
  
  You can see in my Main() function, I'm calling another public static method called "ParseAndSaveDataToDataBase" which is basically contains all the logic to parse and save the data in our Database.
  
  My file is in project folder that is "ALL.csj".
  
  ** Please dont forget to change the file path name in ParseAndSaveDataToDataBase() method before running your project.
  
  ** Update the connection string with your server name, Database name and credentials. It's inside App.config file.
