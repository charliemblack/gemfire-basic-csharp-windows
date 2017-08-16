# GemFire .NET example

In this example we explore a simple GemFire C# client.   The client doesn't do much more than do some basic get and put operations with a basic query.

# Project Structure
I created this Visual Studio Project in my `c:\dev\` directory with the following contents:

```
├───gemfire-basic-csharp-windows
│   ├───data  //This is where all of the data ends up for GemFire
│   ├───GemFireSampleApplication  // The Visual Studio Project File
│   └───scripts // Some basic MS-DOS scripts to control GemFire
└───pivotal-gemfire-native-9.1.1-build.2-Windows-64bit  // The GemFire C++ and C# libraries
```

# The Scripts

In the `scripts` folder there are a couple of commands:
* `clear.bat` - Removes all of the contents of the data directory.
* `configure.bat` - Creates the regions needed for the sample code.
* `shutdownGemFire.bat` - Shuts down all of the GemFire processes.
* `startGemFire.bat` - Starts up GemFire

# Start GemFire
GemFire is in memory data grid solution written in Java so that means we need to install Java before we run GemFire.   Just download and install the latest version of Java 8 : http://www.oracle.com/technetwork/java/javase/downloads/index.html

After installing Java create an environment varible called JAVA_HOME which points to the directory that you have installed Java.   Example `JAVA_HOME=C:\dev\Java\jdk1.8.0_xyz`

Then go download the latest version of GemFire from Pivotal : https://network.pivotal.io/products/pivotal-gemfire  Normally the download is zip file, so just unzip GemFire.   

Once that is done to make our life easier lets add the Java and GemFire bin directories to the path.

example
```
set PATH=C:\dev\Java\jdk1.8.0_xyz\bin;C:\dev\pivotal-gemfire-9.x.y\bin;%PATH%
```

Change Directory to the scripts and run the `startGemFire.bat` and GemFire will be up and running.   Double check and make sure Windows firewall didn't block Java from opening up the required ports.

If this is the first time we started GemFire or fresh launch after a `clear.bat` we need to add the regions that the project will be using.   This is done with the `configure.bat` script.

GemFire comes with a management console called pulse.   That is also started up by default if you would like to see it open:  http://localhost:7070/pulse and login as `admin:admin` the default user name and password.   Of course for production I would recommend changing that password.

![Visual Studio](/images/pulse.png)

# Visual Studio

If we managed to set up the project the same way Visual Studio shouldn't have a problem with finding the GemFire C# libraries.    I tried to make the referances relative to project directory.  If we didn't match Visual Studio makes it easy to add libraries to a project.  Just google `Adding Resources to Visual Studio` with the version of visual studio you are using and add in the GemFire libraries.

Then you are ready to run the application.   Just make sure we are running with the x64 platform.

![Visual Studio](/images/VisualStudio.png)

The output from the program is:

What is in the cache:
```
item - 0 : { "firstName" : "firstName bulk 0",  "lastName" : "lastName 0", "id" : 0 }
...
item - 999 : { "firstName" : "firstName bulk 999",  "lastName" : "lastName 999", "id" : 999 }
```

Number of Objects stored in GemFire (or keys value pairs):
```
Number of Object in GemFire 1000
```
Query for specific object:
```
result { "firstName" : "firstName bulk 100",  "lastName" : "lastName 100", "id" : 100 }
done
```
