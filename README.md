# ARGUS C# Dotnet Client 

This is the official C# dotnet (C# .Net) library for the [ARGUS Engine](https://github.com/Khelechy/argus), this library helps .NET developers and applications seamlessly integrate to the ARGUS Engine, authentication and event listening.

## Install the package 

Ensure you have .NET 6+ installed. 

### Install via .NET CLI

```sh
    dotnet add package Argus.NET
```

### Install via .NET CLI

```sh
    Install-Package Argus.NET
```


### Usage -

```c#

    using Argus
    using Argus.Events
```

### Using the package

```c#
    var argus = new Argus(new ArgusConfig
    {
        Username = "testAdmin",
        Password = "testPassword",
        Host = "localhost",
        Port = 1337
    });

    argus.ArgusEventRaised += HandleArgusevent

    argus.Connect();

    // Define what actions to take when the event is raised.

    void HandleArgusevent(object sender, ArgusEventArgs e)
    {
        Console.WriteLine($"Recieved event with description {e.Event.ActionDescription}");
    }
```