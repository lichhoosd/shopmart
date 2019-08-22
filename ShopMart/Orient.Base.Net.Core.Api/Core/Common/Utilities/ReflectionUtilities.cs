using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Orient.Base.Net.Core.Api.Core.Common.Utilities
{
    public static class ReflectionUtilities
    {
        #region  Assembly Helpers

        /// <summary>
        /// Get  loaded assemblies
        /// </summary>
        /// <returns></returns>
        public static List<AssemblyName> GetReferencedAssemblies()
        {
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblies = currentAssemblies.SelectMany(i => i.GetReferencedAssemblies()).
                Where(i => i.FullName.Contains("Orient.Base.Net.Core.Api")).Distinct().ToList();

            return assemblies;
        }

        /// <summary>
        /// Get  loaded assemblies
        /// </summary>
        /// <returns></returns>
        public static Assembly GetAssembly(string name)
        {
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return currentAssemblies.FirstOrDefault(i => i.FullName.Contains(name));
        }

        /// <summary>
        /// Get dynamic type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetDynamicType(string type)
        {
            var returnType = Type.GetType(type);

            if (returnType == null)
            {
                //Cannot convert type, so maybe type is from dynamic assembly
                returnType = Type.GetType(
                    type,
                    (name) =>
                    {
                        // Returns the assembly of the type by enumerating loaded assemblies
                        // in the app domain            
                        return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(z => z.FullName == name.FullName);
                    },
                    null,
                    true);
            }

            return returnType;
        }

        /// <summary>
        /// Get  assemblies
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return currentAssemblies.Where(a => a.FullName.Contains("Orient.Base.Net.Core.Api"));
        }

        #endregion

        /// <summary>
        /// Get all properties from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllPropertiesOfType(Type type)
        {
            return type.GetProperties().ToList();
        }

        /// <summary>
        /// Get all property name from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetAllPropertyNamesOfType(Type type)
        {
            var properties = type.GetProperties();
            return properties.Select(p => p.Name).ToList();
        }
    }
}
