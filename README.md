# Whetstone

[![License](
http://img.shields.io/:license-mit-blue.svg)](
http://vsisk.mit-license.org)
[![Build status](
https://ci.appveyor.com/api/projects/status/fsrvcqeallu5uft0?svg=true)](
https://ci.appveyor.com/project/sappharx/whetstone)
[![Coverage Status](
https://coveralls.io/repos/github/sappharx/Whetstone/badge.svg?branch=master)](
https://coveralls.io/github/sappharx/Whetstone?branch=master)

Toolbox of functional extension methods and other useful tools for sharpening your .NET code

# Example
Using the `Map()` extension method

    var number = "12"
        .Map(int.Parse)
        .Map(x => x * 2);   // number is an int and has a value of 24

Using the `Map()` extension method with an `IEnumerable<T>`, where you want to map each item

    var numbers = new List<string>() {"1", "2", "3"}
        .Map<string, int>(int.Parse);   // numbers is a List<int>

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
coming soon

# License
MIT: http://vsisk.mit-license.org/