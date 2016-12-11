# Software Engineering Manager - Practical Assignment - Medical Journals

## Assignment
*NOTE: Assignment is cited by memory as I don't recall all of the spcifications*

Design and develop a system allowing publishing of scientific journals and subscribers reading the journals through dedicated desktop application. The journals need to be uploaded by managers as PDF files and displayed to the subscribers in the dedicated app in such manner that does not allow users for copying the journals either by sniffing traffic, browsing files, database, etc.

## Solution

Designed and developed under Ubuntu for cross-platform environment using Mono.

[![Video presentation in YouTube](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/youtube.png)](https://www.youtube.com/watch?v=EEM_8C3mjeo)

### Technologies

* ASP.NET WebAPI 2
* ASP.NET Identity 2
* NHibernate
* AngularJS 2.0 (TypeScript)
* MySQL
* ImageMagick (transforming PDF to image files which are then put in the database)
* ElectronJS (transforming AngularJS frontoffice application to crossplatform desktop app)

### Deliverables

* Backoffice - managers publish content, manage users
* Frontoffice / Website - users register and subscribe to
* Reader application - AngularJS app packed to native desktop cross-platform application for reading journals in a secure way

### Design

[Enterprise Architect Project](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/CrossoverMedicalJournalsArchicteture.eap)

[![Use cases](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Primary Use Cases.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Primary Use Cases.png)

[![Content Lifecycle](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Content Lifecycle.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Content Lifecycle.png)

[![Components](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Components.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Components.png)

[![Class Model](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Class Model.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Class Model.png)

[![Logical Data Model](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Logical Data Model.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Logical Data Model.png)

[![Physical Data Model](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Physical Data Model.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/Physical Data Model.png)

[![Journals Physical Data Model](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/JournalPDM.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/JournalPDM.png)

[![Identity Physical Data Model](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/IdentityPDM.png)](https://raw.githubusercontent.com/shturm/crossover-sem-journals/master/Source/IdentityPDM.png)


## Installation

### Pre-requisites
*best viewed as Markdown*

1) Install and configure MySQL Server v. 5.5. Ensure ports are open and accessible: `mysql -u root -p`
2) Import application database - `Source/server/crossover.sql`
3) Import test database - `Source/server/crossover_test.sql`
4) Change username and password in connection strings in `Source/server/CrossoverSemJournals.WebApi/web.config` and `Source/server/Tests/app.config` OR create user 'uniuser'@'localhost' identified by 'unipass'
5) Install ImageMagick 6.7 for Windows and bin/ folder to PATH (for PDF conversion). Confirm correct installation by executing `convert` in shell. It should display something like 
```
Version: ImageMagick 6.7.7-10 2016-06-01 Q16 http://www.imagemagick.org
Copyright: Copyright (C) 1999-2012 ImageMagick Studio LLC
Features: OpenMP
```
Without correctly installed `imagemagick` utilities the application won't be able to parse PDFs and some tests will fail.
6) Install NodeJS v6.5
7) Install TypeScript v1.8+. Verify installation by running `tsc --version` in shell. It should output something like `Version 2.1.0-dev.20160916`. Anything equal or above 1.8 is fine.
8) Install ElectronJS (to build and run desktop app)
9) Install Electron-Packager (to build EXE version of electron desktop app)

### Build

1) Open solution and build.
2) For front-office client `cd Source/client/frontoffice; npm install` then `tsc`. Note CWD (current working directory) has to be the client project root when you execute `tsc`. Applies for all client projects.
3) For back-office client `cd Source/client/backoffice; npm install` then `tsc`. 
4) For desktop client `cd Source/client/desktop; npm install; tsc;`. If you would like to build EXE run `cd Source/client/desktop; electron-packager DesktopApp --platform win32`

### Run

#### For backend
1) Set WebApi project to be default project if not set
2) Set port for WebApi to be 8080 if not set 
3) Start visual studio and run default project

#### For frontend
1) For front-office `cd Source/client/frontoffice; npm run lite`
2) For back-office `cd Source/client/backoffice; npm run lite`
3) For desktop `cd Source/client/desktop; electron electron-main.js`.

#### Accounts

Publisher username: admin@email.com
Publisher password: 123123

Subscriber username: user@email.com
Subscriber password: 123123

### Test

1) MS CLR treats config files slightly different. Delete/comment out the following snippet from file `Source/server/Tests/Utils/TestUtils.cs`:
```
ConfigurationManager.ConnectionStrings.Add (
			new ConnectionStringSettings (
				"DefaultConnection",
				"Server=localhost;Database=crossover_sem_journals_test;Uid=uniuser;Pwd=unipass;")
		);
```

2) Install NUnit Test Adapter extension for Visual Studio.
3) Update NUnit Nuget package to 3+
4) Ensure connection strings are correct, database is running and accessible. All but 2 tests are Component/Integration and require functioning connection to the database even if the test itself does not hit it.