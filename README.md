# Dr. Sillystringz's Factory

#### _This is an MVC web app that allows to keep track of machines and engeneers.  1/8/2021_

#### By _**Constantine Yakubovski**_ 

## Description 

_This app will demonstrate the use of database and many to many relationships between two classes.  The factory owner will be able to add a list of machines and a list of engineers that work on them. Will be able to delete machines or engineers. Will be able to add engineers without machines and add machines without engeneers._

### SPECS: ###

1. SPEC: Splash page to select list of Machines and enegeneer.

2. SPEC: On Machine page an ability to select any machine and see what enegeneer works on to it.

3. SPEC: On Enegeneer page an ability to select any enegeneer and see what machine the work on.

4. SPEC: Ability to add and delete any machine or engeneer.

5. SPEC: Ability to navigate to any page.


## Installation Requirements

- Install [MySQL Workbench](https://dev.mysql.com/downloads/file/?id=484391)
- Install [MySQl](https://dev.mysql.com/downloads/file/?id=484914)
- Install [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- Install [Visual Studio Code](https://code.visualstudio.com/)
- Install [Git](https://git-scm.com/downloads/)

## Setup
In the Terminal
-  `$ cd ~` - it will navigate to the user's home directory
-  `$ cd desktop`- it will navigate to the desktop
-  `$ git clone` ,then copy/paste https://github.com/faustlarsen/SillyFactory , then press 'enter' - it will create the file on the desktop
-  `$ cd SillyFactory` - it will enter the folder
-  `$ code .` - it will launch VSCode ( text editor ) to open the file
-  `$ touch appsettings.json` - create this file in root directory
- Copy and paste in appsettings.json file: 

```
{
  "ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3306;database=constantine_yakubovski_factory;uid=root;pwd=YourPassword;"
  }
}
```
- Replace 'YourPassword' with MySQL password that was set during installation of MySQL.
- `$ dotnet restore`

## DATABASE SETUP 
Copy and Paste the following commands in the terminal. (exclude '$' and '>')
-  `$ mysql -uroot -pepicodus ` - start MySQL Server 
-  `$ cd ~` - it will navigate to the user's home directory
-  `$ cd desktop`- it will navigate to the desktop
-  `$ cd SillyFactory` - it will enter the folder
-  `$ dotnet ef database update` - it will generate database
- `> exit ` - to exit MySQL
- `$ dotnet restore ` - it will complie the code
- `$ dotnet run ` - it will launch the app 
- Then in console click on (localhost:5000) to view the app in the browser

## ALTERNATIVE DATABASE SETUP
- `$ mysql -uroot -pepicodus` - start MySQL Server 
- Open MySQL Workbench
- Choose 'Administration'
- Then 'Data Import'
- Choose  'Import Self-Contained File"
- In browsing tool to select the constantine_yakubovski_factory.sql file that is in the project
- Start Import

## ALTERNATIVE DATABASE SETUP
-  `$ cd desktop`- it will navigate to the desktop
-  `$ cd SillyFactory` - it will enter the working folder
-  `$ cd Factory` - it will enter the main project
- `$ dotnet ef migrations Initial` - will create database on MySQL Workbench
- `$ dotnet ef database update` 
- `$ dotnet run ` - it will launch the app in (localhost:5000)


## Known Bugs


## Support and contact details

__faustlarsen@gmail.com__

## Technologies Used

-  _C#_

-  _ASP.NET_

-  _MVC_

-  _My SQL_

-  _HTML_

- _Entity_

-  _Written in VS Code_

### License

This software is licensed under the MIT license

Copyright (c) 2020 **_Constantine Yakubovski_**