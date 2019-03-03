---
Title: The ViewModel
CodeTask:
    Path: 10_viewmodel.csharp.csx
    Default: CounterViewModel_10.cs
    Correct: CounterViewModel_20.cs
---

# Getting Started: The ViewModel

Hello and welcome to DotVVM Academy. In this lesson, you'll build a simple Counter application that will allow the users to add or subtract a given step to a value of the counter.

> Programming in DotVVM requires basic knowledge of [HTML](https://www.sololearn.com/Course/HTML/) and [C#](https://www.sololearn.com/Course/CSharp/). If you haven't worked with these languages so far, spend some time learning the basic concepts to understand what's going on.

How DotVVM works?

Every page in DotVVM consists of two files:

* __View__, which is based on HTML syntax and tells DotVVM how the page will look like.
* __ViewModel__, which is a C# class that represents the state of the page (values in the form fields) and handles user interactions (button clicks and so on).

The state in ViewModel is kept in C# properties:

```
public int Result { get; set; }
```

The user actions are handled by public methods.

---

## Tasks

Let's start with the viewmodel. Add two `public int` properties named `Result` and `Difference` in the viewmodel.