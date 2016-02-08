﻿#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 SimplePersistence
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#if PORTABLE40
[assembly: AssemblyTitle("SimplePersistence.UoW Portable .NET 4.0")]
#elif PORTABLE
[assembly: AssemblyTitle("SimplePersistence.UoW Portable")]
#elif DOTNET
[assembly: AssemblyTitle("SimplePersistence.UoW .NET Platform")]
#elif NET20
[assembly: AssemblyTitle("SimplePersistence.UoW .NET 2.0")]
#elif NET35
[assembly: AssemblyTitle("SimplePersistence.UoW .NET 3.5")]
#elif NET40
[assembly: AssemblyTitle("SimplePersistence.UoW .NET 4.0")]
#else
[assembly: AssemblyTitle("SimplePersistence.UoW")]
#endif

[assembly: AssemblyDescription("SimplePersistence.UoW is a framework to help implement the Unit of Work pattern, by exposing interfaces for repositories that can be aggregated in work areas.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Net.JoaoSimoes")]
[assembly: AssemblyProduct("SimplePersistence")]
[assembly: AssemblyCopyright("Copyright © 2016 SimplePersistence")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if !(PORTABLE40 || PORTABLE)

[assembly: ComVisible(false)]

[assembly: Guid("6c633c6b-b379-48c7-92bd-1e006a0ab01e")]

#endif

[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("3.0.0")]
[assembly: AssemblyInformationalVersion("3.0.0-alpha1")]
