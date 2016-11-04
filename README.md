# BrainFuckInterpreter
A rough draft of an interpreter library for BrainFuck written in C#. Also includes a simple console debugger.
The code is pretty rough, hence it being called a rough draft. I have future plans to add a GUI project to the solution for the
purpose of having a fully-featured BrainFuck IDE complete with visual debugger. Who knows if I'll ever get that far.

The purpose of the solution in the first place is purely academic, and seemed like a fun little project since BrainFuck
is a very simple language.

A rundown of the projects in this solution:

BrainFuckDebugger:
This is a command-line debugger. It's simplistic and allows for breakpoints and stepping, as well as viewing
the cells and their values when execution is paused. However, since by default the buffer width of the console is 80,
large BrainFuck programs will be difficult to debug with this. That's why I want an IDE solution.

BrainFuckInterpreter:
This is basically a command line wrapper for BrainFuckInterpreterLib that provides what you'd expect for a command line
BrainFuck interpreter to be able to do.

BrainFuckInterpreterApp:
This got committed by mistake, I forgot this project was in the solution. But basically it was a test application
for testing the API of BrainFuckInterpreterLib.dll.

BrainFuckInterpreterLib:
This is the core of the solution. It's the nuts and bolts of the interpreter. The API allows for input and output through any TextReader/TextWriter object. BrainFuckInterpreter (bf.exe) uses Console.In and Console.Out, for obvious reasons. But you could
take this DLL and make a GUI interpreter application with it. It also provides the debugger functionality used in
BrainFuckDebugger, but once I added that in the code got pretty messy. Lots of changing stuff from private to internal just
so I could get it to work and I haven't refactored anything yet.
