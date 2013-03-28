using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Runtime.InteropServices;

//Bring a window to front if it already exits

namespace Check
{
    class ShowWindowAsTopMost
    {
        /*
         BOOL WINAPI SetWindowPos(
  _In_      HWND hWnd,
         * A handle to the window.
  _In_opt_  HWND hWndInsertAfter,
         * A handle to the window to precede the positioned window in the Z order.
  _In_      int X,  
         * new position of the left side of the window,
         * in client coordinates.
  _In_      int Y,
         * The new position of the top of the window, 
         * in client coordinates.
  _In_      int cx,
         * The new width of the window, in pixels.
  _In_      int cy,
         * The new height of the window, in pixels.
  _In_      UINT uFlags
         * The window sizing and positioning flags.

         */
        [DllImport("User32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,  //
            int Y,
            int cx,
            int cy,
            uint uFlags
            );



        //parameters for hWndInsertAfter 

        static private IntPtr HWND_TOPMOST = new IntPtr(-1);
        static private IntPtr HWND_BOTTOM = new IntPtr(1);
        static private IntPtr HWND_NOTOPMOST = new IntPtr(-1);
        static private IntPtr HWND_TOP = new IntPtr(0);

        //the uFlags
        private const uint SWP_NOSIZE = 0x0001;
        /*
         *Retains the current size (ignores the cx and cy parameters).
         */

        private const uint SWP_NOMOVE = 0x0002;
        /*
         *Retains the current position (ignores X and Y parameters). 
         */

        private const uint SWP_ASYNCWINDOWPOS = 0x4000;
        /*
         * If the calling thread and the thread that owns the window 
         * are attached to different input queues, 
         * the system posts the request to the thread that owns the window. 
         * This prevents the calling thread from blocking its execution
         * while other threads process the request.
         */

        private const uint SWP_DEFERERASE = 0x2000;
        /*
         * Prevents generation of the WM_SYNCPAINT message.
         * ie whether a repaint is necessary
         */

        private const uint SWP_DRAWFRAME = 0x0020;
        /*
         *Draws a frame 
         *(defined in the window's class description) 
         *around the window
         * */

        private const uint SWP_FRAMECHANGED = 0x0020;
        /*
         * Applies new frame styles set using the SetWindowLong function.
         * Sends a WM_NCCALCSIZE message to the window, 
         * even if the window's size is not being changed. 
         * If this flag is not specified, 
         * WM_NCCALCSIZE is sent only when the window's size 
         * is being changed.
         */
        private const uint SWP_HIDEWINDOW = 0x0080;

        /*
         * Hides the window.
         */

        private const uint SWP_NOACTIVATE = 0x0010;

        /*
         * Does not activate the window. 
         * If this flag is not set, the window is activated 
         * and moved to the top of either the topmost or non-topmost group 
         * (depending on the setting of the hWndInsertAfter parameter).
         */

        private const uint SWP_NOCOPYBITS = 0x0100;
        /*
         * Discards the entire contents of the client area. 
         * If this flag is not specified, 
         * the valid contents of the client area are saved and 
         * copied back into the client area after the window is 
         * sized or repositioned.
         */


        private const uint SWP_NOOWNERZORDER = 0x0200;
        /*
         * Does not change the owner window's position in the Z order.
         */

        private const uint SWP_NOREDRAW = 0x0008;
        /*
         * Does not redraw changes. 
         * If this flag is set, no repainting of any kind occurs. 
         * This applies to the client area, the nonclient area 
         * (including the title bar and scroll bars), 
         * and any part of the parent window uncovered as a result of 
         * the window being moved. 
         * When this flag is set, 
         * the application must explicitly invalidate or redraw 
         * any parts of the window and parent window that need redrawing.
         */


        private const uint SWP_NOREPOSITION = 0x0200;
        /*
         * Same as the SWP_NOOWNERZORDER flag.
         */


        private const uint SWP_NOSENDCHANGING = 0x0400;
        /*
         * Prevents the window from receiving 
         * the WM_WINDOWPOSCHANGING message.
         */

        private const uint SWP_NOZORDER = 0x0004;
        /*
         * Retains the current Z order
         * (ignores the hWndInsertAfter parameter).
         */

        private const uint SWP_SHOWWINDOW = 0x0040;
        /*
         *Displays the window.

         */



        public ShowWindowAsTopMost()
        {
        }







        public void setWindowPosition(Form form)
        {
            try
            {

                bool funSuccess = SetWindowPos(form.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
                int errorOfFunc = Convert.ToInt32(
              System.Runtime.InteropServices.Marshal.GetLastWin32Error());
                System.Diagnostics.Debug.WriteLine("\n" + "SetWindowPos" +"\n"
                    + "Success : " + funSuccess + "\n"
                    + "error   : " + errorOfFunc +"\n"
                    );
            }

            catch (System.EntryPointNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Function not found in dll " +
                    " Function name: SetWindowPos" +
                        "Dll Name : User32.dll"

                );

            }
        }




    }
}
