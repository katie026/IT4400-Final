using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<Window> myWindows;
        public Boolean doingCloseAll = false;

        public App()
        {
            myWindows = new List<Window>();

            // I am taking over the creation of the main window here in the App
            // This means I had to modify the App.xaml so on startup it doesn't
            // create a window.

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            mainWindow.myApp = this;  // In the window have a pointer to the App object so the App methods can be called from the window.
            myWindows.Add(mainWindow);  // Add the main window to the list of windows.
            mainWindow.Show();
        }

        public void AddWindow(Window myWindow)
        {
            // put the window in the list of windows
            myWindows.Add(myWindow);

            // NOTE:  myWindows is a List<Window> type.  It is a List that holds Window type objects.
            // All of my windows are derived from the Window class.  So, even though
            // there are different derived classes of Window (MainWindow, WindowStyle1, and WindowStyle2)
            // all of the windows can be remembered in a List that holds Window type objects.
        }

        public void RemoveWindow(Window myWindow)
        {

            // doingCloseAll status is needed because if we are
            // closing all the windows, when those windows close
            // they will fire a Closed event which will call RemoveWindow
            // which will modify the list while we are doing a foreach
            // through it and that will mess up walking through the list.

            // myWindows.Remove(myWindow) should only be called when
            // an individual window is being closed by the X or the
            // window close option on the window.  This will ensure
            // the list only contains valid/active windows.
            if (!doingCloseAll)
            {
                myWindows.Remove(myWindow);
            }
        }

        public void CloseAllWindows()
        {
            doingCloseAll = true;

            // Go through the list of windows and close them.
            foreach (Window myWindow in myWindows)
            {
                if (myWindow != null)
                {
                    myWindow.Close();
                }
            }

            // Clear out all the items in the list of windows.
            myWindows.Clear();
        }
    }
}
