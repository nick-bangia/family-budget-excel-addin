using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using FamilyBudget.Common.Attributes;

namespace FamilyBudget.Common
{
    public static class APIResolver
    {
        public static Type ResolveTypeForInterface(Type myInterface)
        {
            // get the assembly that implements the api contracts by looking for the DataImplementationAssemblyAttribute
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            Assembly dataAssembly = null;
            foreach (string assemblyFile in Directory.GetFiles(assemblyPath, "*.dll"))
            {
                Assembly anAssembly = Assembly.LoadFile(assemblyFile);
                if (anAssembly.GetCustomAttributes(typeof(DataImplementationAssemblyAttribute), false).Count() > 0)
                {
                    // if we find an assembly that has the right attribute specified, then break out of this loop
                    dataAssembly = anAssembly;
                    break;
                }
            }

            if (dataAssembly != null)
            {
                // check if it is indeed an interface, and then return the first type that implements it
                if (myInterface != null && myInterface.IsInterface)
                {
                    Type implementation = myInterface.IsImplemented(dataAssembly);
                    if (implementation != null)
                    {
                        // found an implementation of the requested interface!
                        return implementation;
                    }
                    else
                    {
                        // no implementation found
                        return null;
                    }
                }
                else
                {
                    // the passed in interface is not actually an interface
                    return null;
                }
            }

            // if there is no data assembly
            return null;
        }

        public static Type IsImplemented(this Type byInterface, Assembly withinAssembly)
        {
            // loop through the types of the assembly
            foreach (Type aType in withinAssembly.GetTypes())
            {
                // check each type's interfaces, and if a match is found, return the overall type (aType)
                Type[] interfaces = aType.GetInterfaces();
                foreach (Type iType in interfaces)
                {
                    if (iType == byInterface)
                    {
                        // if the interface is found, return true
                        return aType;
                    }
                }
            }

            // if code gets here, this interface is not implemented
            return null;
        }
    }
}
