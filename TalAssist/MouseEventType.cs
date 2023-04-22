using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalAssist
{
    public enum MouseEventType : uint
    {
        LeftButtonDown = 0x0002,
        LeftButtonUp = 0x0004,
        RightButtonDown = 0x0008,
        RightButtonUp = 0x0010,
        MiddleButtonDown = 0x0020,
        MiddleButtonUp = 0x0040,
        MouseMove = 0x0001,
    }
}
