#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Kimicu.ExcelImporter
{
    public static class ParserUtils
    {
        public static IGoogleSheetParser GetParserByName(string parserName, GameSettings gameSettings)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IGoogleSheetParser).IsAssignableFrom(p) && p.Name == parserName)
                .ToArray();

            foreach (Type type in types)
            {
                ConstructorInfo[] constructors = type.GetConstructors();
                foreach (ConstructorInfo constructor in constructors)
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    if (parameters.Length == 1 && parameters[0].Name.ToLower() == "gamesettings")
                    {
                        return (IGoogleSheetParser)Activator.CreateInstance(type, gameSettings);
                    }
                }
            }

            return null;
        }
    }
}
#endif