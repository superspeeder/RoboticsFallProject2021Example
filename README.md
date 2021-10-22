# RoboticsFallProject2021Example


This repository contains code written during the programming lessons for c# for the fall project during the fall in the 2021-2022 year of robotics.

# C# Robotics Cheat Sheet

## Basics

### Required usings
The using statements that are **absolutely needed** for the file containing the main class.
```cs
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
```

### Sleeping the thread
When you want to sleep for a period of time, use the namespace `System.Threading` like so:
```cs
using System.Threading;
```
and sleep the thread for a number of milliseconds like using this function:
```cs
System.Threading.Thread.Sleep(milliseconds);
```

## Motor Controls
### Usings Required
To work with motor controllers, you will need to use these namespaces: `CTRE.Phoenix`, `CTRE.Phoenix.MotorControl`, `CTRE.Phoenix.MotorControl.CAN`. To do so just copy this to the top of your file where the rest of the using statements are:
```cs
using CTRE.Phoenix;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
```

> If this is not being done in the main file, then you need to use the namespaces `Microsoft.SPOT` and `Microsoft.SPOT.Hardware` as well.

### Connecting to Motors

To connect to a motor, you need to create the appropriate motor controller object for the hardware in use. For our purposes in the fall project, we are only using TalonSRX motor controllers. To create the motor, just create an instance of the `TalonSRX` class (using the `CTRE.Phoenix.MotorControl.CAN` namespace allows us to omit that part from the fully qualified name. If you don't use that namespace you would reference it as `CTRE.Phoenix.MotorControl.CAN.TalonSRX`. The TalonSRX class's constructor requires that the ID of the controller be passed in. For working with HERO boards, this ID will be set through Phoenix Lifeboat. For example, to create an object representing a TalonSRX motor controller with the ID of 0 can be done like so:
```cs
TalonSRX motor = new TalonSRX(0);
```

A motor can be configured programmatically, and generally we use the method `ConfigFactoryDefault()` on each motor controller object after their creation to ensure we don't have weird settings leftover. 

### Controlling Motors

After creating the object, there are a number of different modes that can be used to control the motor, but the one that we will focus on is `PercentOutput`. The control modes are found in the enum called `ControlMode`, which is found within the `CTRE.Phoenix.MotorControl` namespace. To set the value for a motor controller, the `Set(ControlMode, float)` method is called on the motor controller object. The first argument is the control mode, or `ControlMode.PercentOutput` in our case. The second value is the value to set it to, which in our case is a value from -1.0 to 1.0 with -1.0 being 100% backwards and 1.0 being 100% forwards. An example of this:
```cs
motor.Set(ControlMode.PercentOutput, 1.0); // go forwards at 100%
System.Threading.Thread.Sleep(1000); // wait 1 second
motor.Set(ControlMode.PercentOutput, -1.0); // go backwards at 100%
System.Threading.Thread.Sleep(1000); // wait 1 second
motor.Set(ControlMode.PercentOutput, 0.0); // stop the motor
```
This example may not work (untested as of writing this), but that would be due to missing watchdog updates.

Watchdog is a tool which is used internally by CTRE to make sure that the motor stops if the code crashes, requiring us to send a message periodically to keep the controllers enabled. This is done with the following code each iteration of the mainloop:
```cs
CTRE.Phoenix.Watchdog.Feed();
```

## Suggested Practices

### Mainloops

Use a mainloop like this one (example, missing code within the loop other than the sleep statement)
```cs
while (true) { // loop forever
  // insert code here
  
  System.Threading.Thread.Sleep(20); // sleep for 20 milliseconds
}
```

The 20 milliseconds allows you to approximate the delta time (length of time) of each iteration of the loop since the other code should take <1 ms, at least at the moment. 

## Common Errors

If using Visual Studio and the HERO template projects are missing: reinstall the CTRE libraries with the Phoenix installer (remember to close visual studio first).

If using Visual Studio and an error message similar to the following error appears: reinstall the CTRE libraries with the Phoenix installer until it works (remember to close visual studio first).

![image](https://user-images.githubusercontent.com/15852217/138372886-9b71ad96-884f-4879-a263-01c02b8c955a.png)


If visual studio throws this popup when starting it, ignore it.

![image](https://user-images.githubusercontent.com/15852217/138531362-695af358-c0fd-4884-8e94-79974329237c.png)
