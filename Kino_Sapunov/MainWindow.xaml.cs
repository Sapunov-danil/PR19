using Kino_Sapunov.Pages;
using Mysqlx.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kino_Sapunov
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public enum Pages 
        {
            kinoteatr,
            afisha
        }

        public MainWindow()
        {
            InitializeComponent();
            init = this;
        }

        public void OpenPages(Pages page)
        {
            switch (page)
            {
                case Pages.kinoteatr:
                    frame.Navigate(new KinoteatrPage());
                    break;
                case Pages.afisha:
                    frame.Navigate(new AfishaPage());
                    break;
            }
        }
    }
}
