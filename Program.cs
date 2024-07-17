using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleMouseRotationCircle
{
    class Program
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int UpdateInterval = 5; // Interval for updating the cursor position (ms)

        private static double CircleRadius = 100.0; // Radius of the circle around the mouse cursor
        private static double RotationSpeed = 10.0; // Rotation speed in degrees per second
        private static int AutoStopDelay = 20000; // Time until automatic stop after starting (20 seconds)

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            Console.Title = "Rotate Mouse Cursor in Perfect Circle";
            Console.CursorVisible = false;

            Console.WriteLine("Enter the rotation speed in degrees per second (must be greater than 40):");
            RotationSpeed = GetRotationSpeed();

            Console.WriteLine("Enter the radius of the circle (must be greater than 15 pixels):");
            CircleRadius = GetCircleRadius();

            Console.WriteLine("Enter the auto stop delay in milliseconds (default is 20000 ms):");
            AutoStopDelay = GetAutoStopDelay();

            Console.WriteLine("Preparing to start rotation. Move the mouse cursor to the center of the screen.");
            CenterMouse();

            Console.WriteLine($"Rotation starts around current position: ({Console.WindowWidth / 2}, {Console.WindowHeight / 2})");
            Console.WriteLine($"Rotation speed: {RotationSpeed} degrees per second");
            Console.WriteLine($"Circle radius: {CircleRadius} pixels");
            Console.WriteLine($"Auto stop delay: {AutoStopDelay} milliseconds");

            Console.WriteLine("Press Enter to start rotation.");
            Console.ReadLine();

            POINT initialCursorPos;
            GetCursorPos(out initialCursorPos);

            try
            {
                StartRotation(initialCursorPos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press Enter to stop manually.");
                Console.ReadLine();

                // Release mouse button
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
        }

        private static void StartRotation(POINT centerPoint)
        {
            double angle = 0;
            double step = RotationSpeed * UpdateInterval / 1000.0; // Convert rotation speed to degrees per millisecond
            DateTime startTime = DateTime.Now;
            TimeSpan elapsed;

            while (true)
            {
                elapsed = DateTime.Now - startTime;

                if (elapsed.TotalMilliseconds >= AutoStopDelay)
                {
                    // Automatically stop after specified delay
                    break;
                }

                angle += step;

                double radians = angle * Math.PI / 180.0;
                int newX = centerPoint.X + (int)Math.Round(CircleRadius * Math.Cos(radians));
                int newY = centerPoint.Y + (int)Math.Round(CircleRadius * Math.Sin(radians));

                SetCursorPos(newX, newY);

                // Hold down left mouse button
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                Thread.Sleep(UpdateInterval);
            }
        }

        private static void CenterMouse()
        {
            int centerX = Console.WindowWidth / 2;
            int centerY = Console.WindowHeight / 2;

            SetCursorPos(centerX, centerY);
            Thread.Sleep(500); // Wait half a second in the center
        }

        private static double GetRotationSpeed()
        {
            double speed;
            while (true)
            {
                try
                {
                    if (double.TryParse(Console.ReadLine(), out speed))
                    {
                        if (speed > 40)
                        {
                            return speed;
                        }
                        else
                        {
                            Console.WriteLine("Rotation speed must be greater than 40 degrees per second. Please enter a valid value:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid rotation speed:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static double GetCircleRadius()
        {
            double radius;
            while (true)
            {
                try
                {
                    if (double.TryParse(Console.ReadLine(), out radius))
                    {
                        if (radius > 15)
                        {
                            return radius;
                        }
                        else
                        {
                            Console.WriteLine("Radius must be greater than 15 pixels. Please enter a valid value:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid radius:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static int GetAutoStopDelay()
        {
            int delay;
            while (true)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out delay))
                    {
                        if (delay >= 0)
                        {
                            return delay;
                        }
                        else
                        {
                            Console.WriteLine("Auto stop delay must be a non-negative integer. Please enter a valid value:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid auto stop delay in milliseconds:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        // Set mouse cursor position
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
    }
}
