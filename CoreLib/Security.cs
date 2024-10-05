using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    class Security : Disposer
    {
        private readonly string _encryptionPassword = "IDKitsSupposedTOBEaStrongPass$#@";

        public string encryptionPassword { get { return _encryptionPassword; } }
    }
}
