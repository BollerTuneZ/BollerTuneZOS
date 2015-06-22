using System;

namespace BTZ.Tests.Infrastructure
{
    /// <summary>
    /// Jonas Ahlf 21.06.2015 11:36:09
    /// </summary>
    public interface ITest
    {
        event EventHandler OnTestFinish;
        void Start();
    }
}