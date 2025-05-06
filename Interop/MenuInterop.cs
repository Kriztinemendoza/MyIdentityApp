using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentityApp.Interop
{
    public static class MenuInterop
    {
        public static Action? OnCloseMenu;

        [JSInvokable("CloseMenuFromOutside")]
        public static void CloseMenuFromOutside()
        {
            OnCloseMenu?.Invoke();
        }
    }
}
