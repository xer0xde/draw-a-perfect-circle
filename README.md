# Draw a Perfect Circle with C#

This C# console application allows you to rotate the mouse cursor in a perfect circle around its current position on the screen. It's a simple project aimed at learning how to interact with the Windows API (`user32.dll`) to control the mouse cursor programmatically.

## Requirements
- .NET Framework (compatible with .NET Core as well)
- Windows operating system

## How It Works
1. **Setup**: Upon starting the application, you will be prompted to enter three parameters:
   - Rotation speed in degrees per second (must be greater than 40).
   - Radius of the circle in pixels (must be greater than 15).
   - Auto stop delay in milliseconds (default is 20000 ms).

2. **Initialization**: After entering these parameters, move your mouse cursor to the desired center position on the screen. The program will wait for 10 seconds before starting to allow you to position the cursor.

3. **Rotation**: Once the setup is complete and the cursor is positioned, the program starts rotating the cursor in a circle around the center position at the specified speed and radius.

4. **Stopping**: To stop the rotation manually, press Enter in the console window. The program will release the left mouse button if it was pressed during rotation.

## Features
- **Dynamic Setup**: Adjust rotation speed, circle radius, and auto stop delay interactively.
- **Precision Control**: Uses trigonometric calculations to ensure a smooth and accurate circle movement.
- **Error Handling**: Handles invalid user inputs gracefully with prompts for correct values.

## Usage
1. Clone the repository to your local machine.
2. Open the project in Visual Studio or your preferred IDE.
3. Build and run the application.
4. Follow the prompts in the console window to set up and start the rotation.

Enjoy learning and experimenting with controlling the mouse cursor programmatically in C#!
