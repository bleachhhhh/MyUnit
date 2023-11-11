using System;
using System.Arithmetic.Tests.Attributes;
using System.Linq;
using System.Reflection;
using MyUnit.Attributes;
using MyUnit.Excections;

namespace MyUnit
{
    public static class MyTestRunner
    {
        public static event Action<string, string> TestFailed;
        public static event Action<string, string> TestPassed;

        public static void Run(Type type)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                RunTestMethod(method);
            }
        }

        private static void RunTestMethod(MethodInfo method)
        {
            var instance = Activator.CreateInstance(method.DeclaringType);
            var factAttribute = method.GetCustomAttribute<MyFactAttribute>();
            var inlineDataAttributes = method.GetCustomAttributes<MyInlineDataAttribute>();
           
            string testName = null;
            try
            {
                if (factAttribute != null && inlineDataAttributes.Any())
                {
                    throw new TestFailureException($"Некорректная формулировка метода {method.Name}");
                }
                if (factAttribute != null)
                {
                    RunTestMethodAsFact(method, instance, out testName);
                    TestPassed?.Invoke(testName, string.Empty);
                }
                if(inlineDataAttributes.Any())
                {
                    
                    foreach(var attribute in inlineDataAttributes)
                    {
                        RunTestMethodAsInlineData(method, instance, attribute, out testName);
                        TestPassed?.Invoke(testName, string.Empty);
                    }
                }
               
            }
            catch (TargetInvocationException ex)
            when (ex.InnerException is TestFailureException)
            {
                TestFailed?.Invoke(testName, ex.InnerException.Message);
            }
        }

        private static void RunTestMethodAsInlineData(MethodInfo method, object instance, MyInlineDataAttribute attribute, out string testName)
        {
            var argList = (attribute).Arguments;
            ValidateTestMethodAndThrow(method, argList);

            var argumentsAsString = string.Join(", ", argList.Select(o => o.ToString()));
            testName = $"{method.GetTestName()} ({argumentsAsString})";

            method.Invoke(instance, attribute.Arguments);
        }

        private static void RunTestMethodAsFact(MethodInfo method, object instance,out string testName)
        {
            testName = $"{method.GetTestName()}";
            if(method.GetParameters().Any())
            {
                throw new TestFailureException($"В MyFact не должно быть аргументов {method.Name}");
            }
            method.Invoke(instance, null);
        }
        private static void  ValidateTestMethodAndThrow(MethodInfo method,object[] argList) 
        {
            var methodParameters = method.GetParameters();
            if (!argList.Any())
            {
                throw new TestFailureException($"Отсутствуют аргументы InlineData {method.GetTestName()}");
            }
            if (!methodParameters.Any())
            {
                throw new TestFailureException($"Отсутствуют аргументы метода {method.GetTestName()}");
            }
            if (argList.Length != methodParameters.Length)
            {
                throw new TestFailureException($"количество аргументов не совпадает {method.GetTestName()}");
            }
            for (int i = 0; i < argList.Length; i++)
            {
                if (argList[i].GetType() != methodParameters[i].ParameterType)  
                {
                    throw new TestFailureException($"Типы аргументов не совпадают. Ожидаемый тип : {method.GetTestName()}.{argList[i].GetType().Name } Полученный тип: {methodParameters[i].ParameterType.Name}");
                }
            }
        }
    }
}
