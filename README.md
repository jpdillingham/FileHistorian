# FileHistorian

[![Build status](https://ci.appveyor.com/api/projects/status/ywkq1g7ra43p76p9?svg=true)](https://ci.appveyor.com/project/jpdillingham/filehistorian)
[![Build Status](https://travis-ci.org/jpdillingham/FileHistorian.svg?branch=master)](https://travis-ci.org/jpdillingham/FileHistorian)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/jpdillingham/filehistorian/blob/master/LICENSE)

A C# .NET application to keep track of changes to a filesystem.

## Why?

To keep track of which files are added, deleted and modified on your computer.

## Installation

Download the [latest release](https://github.com/jpdillingham/FileHistorian/releases) and extract it to the target machine.

The application will run in daemon mode by default, meaning scans will be executed once per day at the configured offset time.  

A single scan can be performed using the ```--run-once``` argument, allowing for greater scheduling flexibility when combined with the Windows task scheduler or Unix Cron.

To install the application as a service on Windows platforms, execute the following command:

```
 FileHistorian.exe --install-service
 ```

 This will install a Windows service named "FileHistorian", which can then be started from the Services console or by issuing the command ```net start FileHistorian``` from a command line.

 ## Configuration

 All configuration settings are stored in ```FileHistorian.exe.config```, located in the root application directory.

 ### Application

 The ```fileHistorian``` section contains all application settings:

 ```xml
   <fileHistorian>
    <ScanTime midnightOffset="00:00:00" />
    <Directories>
      <add path="c:\dir\" />
      <add path="c:\directory 2\" />
    </Directories>
  </fileHistorian>
 ```

 The ```ScanTime``` element is used to determine when the daily scan is to be started.  The ```midnightOffset``` attribute is the time span from midnight of the current day at which the scan is to start.

 For example, if you want the scan to begin at 3:30 AM, the ```ScanTime``` element would look like the following:

 ```xml
 <ScanTime midnightOffset="03:30:00" />
 ```

 The ```Directories``` element contains the list of directories to be scanned, in the order in which they are to be scanned.

 ### Database

 The Entity Framework configuration section must be modified to reflect your database configuration.  The default configuration is for SQL server:

 ```xml
   <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="Server=SQL;Database=FileHistorian;User=FileHistorian;Password=FileHistorian;" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
 ```

 Simply change the connection string to reflect your environment.  

 Entity Framework is capable of connecting to all sorts of databases, however the application is currently only tooled for SQL server.  If you'd like support for a different platform please open an [issue](https://github.com/jpdillingham/FileHistorian/issues)
 listing your desired platform and I'll do my best to incorporate it.

## Roadmap

- [x] Basic scaffolding (service implementation, cross cutting concerns)
- [x] Data (Entity Framework implementation)
- [x] Scanning
- [ ] Detailed scan scheduling
- [ ] Diff (scan comparison) functionality/CLI
- [ ] Web service(s)
- [ ] Web UI
