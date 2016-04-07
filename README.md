# Whetstone

[![AppVeyor](
https://img.shields.io/appveyor/ci/sappharx/Whetstone.svg?style=flat-square)](
https://ci.appveyor.com/project/sappharx/whetstone)
[![Coveralls](
https://img.shields.io/coveralls/sappharx/Whetstone.svg?style=flat-square)](
https://coveralls.io/github/sappharx/Whetstone?branch=master)
[![NuGet](
https://img.shields.io/nuget/v/Whetstone.svg?style=flat-square)](
https://www.nuget.org/packages/Whetstone/)
[![License](
http://img.shields.io/:license-mit-blue.svg?style=flat-square)](
http://vsisk.mit-license.org)

Toolbox of functional extension methods and other useful tools for sharpening your .NET code

# Example
Using the `Map()` extension method

    var number = "12"
        .Map(int.Parse)
        .Map(x => x * 2);   // number is an int and has a value of 24

# Motivation
After watching [Functional Programming with C#](
https://app.pluralsight.com/library/courses/functional-programming-csharp)
by Dave Fancher on Pluralsight,
I was inspired to create my own library using the concepts I learned.
I've added a few things that I feel can come in handy quite often.

# Installing
This project is available as a [NuGet package](
https://www.nuget.org/packages/Whetstone/), so in Visual Studio you can just run
`Install-Package Whetstone` in the Package Manager Console, as well as
right-clicking in the Solution Explorer and selecting 'Manage NuGet Packages...'
then searching for 'Whetstone'.

# API Reference
coming soon

# Contributing
Fork it. Fix or improve it. Submit a pull request.

# License
MIT: http://vsisk.mit-license.org/
