using System;
using System.Collections.Generic;
using System.Text;

namespace MyUnit.Excections
{
    internal class TestFailureException : Exception
    {
        public TestFailureException(string message) : base(message)
        {

        }
    }
}
