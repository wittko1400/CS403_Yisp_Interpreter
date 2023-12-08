# CS403_Yisp_Interpreter
This Yisp interpreter is based off a variant of Lisp designed for the CS403 class

## Development
Code from a previosly build Lox interpreter in C# was refactored to be a Yisp Interpreter

## Install/Build
Start with installing the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download), then add it into your system enivronment path variables

It can be added to environment path variables by searching system environment variables in the search bar and then selecting Environment Path and editing the path.

Next install an C# IDE such as [Visual Studio](https://visualstudio.microsoft.com/).

Finally clone the Yisp Interpreter repo into where you stored the repos folder of Visual Studio
```
git clone https://github.com/wittko1400/CS403_Yisp_Interpreter
```
## Usage
Once inside Visual Studio open the solution file Lox_Interpreter, then build and clean

Next Run the Yisp_Interpreter.
When run without an argument it operates as a <abbr title="read-eval-print loop">REPL</abbr> prompt which runs until it encounters an exit code. Otherwise, when given a Lox source file it will attempt to execute it and then exit.

## Testing


[Test Output](Yisp_Interpreter/expectedtestoutput.txt)
