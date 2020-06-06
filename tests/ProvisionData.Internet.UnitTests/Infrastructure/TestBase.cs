namespace ProvisionData.Internet.UnitTests
{
    using NodaTime;
    using System;

    public class TestBase : IDisposable
    {
        private Boolean _disposed;

        public ITestableClock Clock { get; }
        public IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            Clock = new TestableClock(SystemClock.Instance);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free managed resources.
                }

                // Free unmanaged resources.
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
