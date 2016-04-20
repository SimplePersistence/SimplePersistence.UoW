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

#if NET40 || NET45
using System.Runtime.InteropServices;
#endif

[assembly: AssemblyTitle("SimplePersistence.UoW")]
[assembly: AssemblyDescription("SimplePersistence.UoW is a framework to help implement the Unit of Work pattern, by exposing interfaces for repositories that can be aggregated in work areas.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Net.JoaoSimoes")]
[assembly: AssemblyProduct("SimplePersistence")]
[assembly: AssemblyCopyright("Copyright © 2016 SimplePersistence")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if NET40 || NET45

[assembly: ComVisible(false)]

[assembly: Guid("6c633c6b-b379-48c7-92bd-1e006a0ab01e")]

#endif

[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("3.2.0")]
[assembly: AssemblyInformationalVersion("3.2.0")]
