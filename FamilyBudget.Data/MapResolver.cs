using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FamilyBudget.Data
{
    public static class MapResolver
    {
        public static Type ResolveTypeForInterface(Type myInterface)
        {
            // attempt to get the typeName passed in as an actual type
            Type requestedInterface;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            try
            {
                // attempt to find the type in the assembly that matches this name
                requestedInterface = currentAssembly.GetType(myInterface.FullName);
            }
            catch (Exception)
            {
                // no such type by that name exists
                return null;
            }

            // check if it is indeed an interface, and then return the first type that implements it
            if (requestedInterface.IsInterface)
            {
                Type implementation = requestedInterface.IsImplemented(currentAssembly);
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
