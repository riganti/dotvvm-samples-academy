---
Title: Getting Started
Moniker: getting-started
CodeTask:
    Path: 10_viewmodel.csharp.csx
    Default: CounterViewModel_10.cs
    Correct: CounterViewModel_20.cs
---

# Getting Started

Hello and welcome to DotVVM Academy. In this lesson, you'll build a simple Counter application that will allow the users to add or subtract a value repeatedly.

> Programming in DotVVM requires basic knowledge of [HTML](https://www.sololearn.com/Course/HTML/) and [C#](https://www.sololearn.com/Course/CSharp/). If you haven't worked with these languages so far, spend some time learning the basic concepts to understand what's going on.

How DotVVM works?

Every page in DotVVM consists of two files:

* a __view__, which is based on the HTML syntax and describes what the page will look like, and
* a __viewmodel__, which is a C# class that describes the state of the page (e.g. values in the form fields) and handles user interactions (e.g. button clicks).

The state of the page is defined by C# properties in the viewmodel:

```csharp
public int Result { get; set; }
```

---

## Tasks

Let's start with the viewmodel. 

- Add two `public int` properties named `Result` and `Difference` to the viewmodel.