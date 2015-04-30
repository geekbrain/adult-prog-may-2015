using System;

namespace WsSoap
{
    public class WsSoapException: Exception
    {
        public WsSoapException(string message)
            : base(message)
        { }
    }
}