using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class Disposer : IDisposable
    {
        private bool _disposed = false;

        // Destructor (Finalizer)
        ~Disposer()
        {
            Dispose(false); // Ensure resources are cleaned up
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent the finalizer from running
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free managed resources here (e.g., database connections, streams)
                }

                // Free unmanaged resources here (if any)

                _disposed = true;
            }
        }
    }
}
