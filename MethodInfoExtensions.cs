using System;
using System.Reflection;

internal static class MethodInfoExtensions
{
	public static string GetTestName(this MethodInfo method)
    {
		return $"{ method.DeclaringType.Name}.{ method.Name} "


	}
}
