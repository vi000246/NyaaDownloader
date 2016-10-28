using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NyaaRSSreader
{
    /// <summary>
    /// 用來操控Form的行為
    /// </summary>
    public class FormControl
    {
        //參數說明https://msdn.microsoft.com/zh-tw/library/windows/desktop/ms633548(v=vs.85).aspx
        private const int SW_SHOWNOACTIVATE = 8;
        //參數說明https://msdn.microsoft.com/zh-tw/library/windows/desktop/ms633545(v=vs.85).aspx
        private const int HWND_BOTTOM = 0;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,             // Window handle
             int hWndInsertAfter,  // -1:將視窗置頂 1:將視窗放在底部 0:將視窗放在上層 -2:在置頂視窗之後
             int X,                // Horizontal position
             int Y,                // Vertical position
             int cx,               // Width
             int cy,               // Height
             uint uFlags);         // Window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            //設定視窗出現順序跟是否Focus
            SetWindowPos(frm.Handle.ToInt32(), HWND_BOTTOM,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }
    }
}
