using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;


namespace RoboticsFallProject2021
{
    public class Program
    {
        public static void Main()
        {

            GameController gamepad = new GameController(UsbHostDevice.GetInstance(0));
            TalonSRX motor = new TalonSRX(0);
            motor.ConfigFactoryDefault();
            /* simple counter to print and watch using the debugger */
            int counter = 0;
            /* loop forever */
            while (true)
            {
                /* print the three analog inputs as three columns */
                Debug.Print("Counter Value: " + counter);

                /* increment counter */
                ++counter; /* try to land a breakpoint here and hover over 'counter' to see it's current value.  Or add it to the Watch Tab */

                CTRE.Phoenix.Watchdog.Feed();

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
