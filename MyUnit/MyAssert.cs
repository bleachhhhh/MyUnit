using MyUnit.Excections;
using System;

namespace MyUnit
{
    
    public static  class MyAssert
    {
        public static  void Equal(object expected, object actual)
        {
            if (!expected.Equals(actual))
                throw new TestFailureException($"Ожидалось значение {expected}, но получено {actual}");
        }
        public static void Throws<TExcpetion>(Action testMethod) where TExcpetion : Exception
        {
            //bool ExceptionWastrown = false;
            //var type=typeof(TExcpetion);
            Exception except = null;
            try
            {
                testMethod();
                if (except == null)
                {
                    throw new TestFailureException("Исключение не вызвано");
                }
            }
            catch (TExcpetion )
            {
            }
            catch (Exception ex)
            {
                throw new TestFailureException($"Вызвано:{ex.GetType().Name} Ожидалось: {typeof(TExcpetion).Name}");
            }
        }
        public static void True(bool check)
        {
            MyAssert.Equal(true, check);
        }
        public static void False(bool check)
        {
            MyAssert.Equal(false, check);
        }
    }
}
