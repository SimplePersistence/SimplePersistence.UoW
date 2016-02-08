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
