using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.Integration;
using Microsoft.Win32;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Diagnostics;
namespace kibiomer_app
{
    public class KeystrokMessageFilter : System.Windows.Forms.IMessageFilter
    {
        public KeystrokMessageFilter() { }
        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == 256 /*0x0100*/))
            {
                switch (((int)m.WParam) | ((int)Control.ModifierKeys))
                {
                    case (int)(Keys.Control | Keys.Alt | Keys.K):
                        //MessageBox.Show("You pressed ctrl + alt + k");                    
                        break;
                    //This does not work. It seems you can only check single character along with CTRL and ALT.
                    //case (int)(Keys.Control | Keys.Alt | Keys.K | Keys.P):
                    //    MessageBox.Show("You pressed ctrl + alt + k + p");
                    //    break;
                    case (int)(Keys.Control | Keys.C): //MessageBox.Show("You pressed ctrl+c");
                        break;
                    case (int)(Keys.Control | Keys.V): //MessageBox.Show("You pressed ctrl+v");
                        break;
                    case (int)Keys.Up: //MessageBox.Show("You pressed up");
                        break;
                    case (int)Keys.F1:
                        try
                        {

                        }
                        catch
                        {

                        }
                        break;
                }
            }
            return false;
        }
    }
}
