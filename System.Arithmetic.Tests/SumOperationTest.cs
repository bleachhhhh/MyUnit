using MyUnit;
using MyUnit.Attributes;
using System.Arithmetic.Tests.Attributes;


namespace System.Arithmetic.Tests
{
    public class SumOperationTest
    {
        [MyInlineData(1, 1, 2)]
        [MyInlineData(2, 2, 4)]
        public void OnePlusOne_EqualsTwo(int first, int second, int expected)
        {
            //Arrange+Act
            var result = first + second;

            //Assert
            MyAssert.Equal(expected, result);
        }
        [MyFact]
        public void TwoTimesTwo_EqualsFour()
        {
            // Arrange + act
            var result = 2 * 2;
            // Assert
            MyAssert.Equal(4, result);
        }
        [MyFact]
        public void DivideByZero()
        {
            MyAssert.Throws<DivideByZeroException>(() => Div());
        }
        public void Div()
        {
            int a = 1;
            int b = 0;
            int res = a / b;
            
        }
        [MyFact]
        public void OneEqualsOne()
        {
            MyAssert.True(1 == 1);
        }
        [MyFact]
        public void TwoIsMoreThanThree_False()
        {
            MyAssert.False(2>3);
        }
        //[MyFact]
        //[MyInlineData]
        //public void InvalideAttridute_TestRunFails()
        //{
        //}
        [MyInlineData(1, 1, 2)]
        public void MethodWithoutArguments(int a, int b, int c)
        {

        }
        
    }
}
