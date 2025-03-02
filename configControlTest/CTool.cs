using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configControlTest
{
    static class CTool
    {
        public static Control? GetControl(Control control, string name)
        {
            Control? retControl = null;
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                {
                    retControl = c;
                    break;
                }
                else
                {
                    Control? subControl = CTool.GetControl(c, name);
                    if (subControl != null)
                    {
                        retControl = subControl;
                        break;
                    }
                }
            }
            return retControl;
        }
    
        public static Control? GetControl(Control control, string name, 
            List<Type> excludeInTypes)
        {
            Control? retControl = null;
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                {
                    retControl = c;
                    break;
                }
                else
                {
                    if(!excludeInTypes.Contains(c.GetType()))
                    {
                        Control? subControl = CTool.GetControl(
                            c, name, excludeInTypes);
                        if (subControl != null)
                        {
                            retControl = subControl;
                            break;
                        }
                    }
                }
            }
            return retControl;
        }
    }
}
