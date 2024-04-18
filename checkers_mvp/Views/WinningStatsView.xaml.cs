using checkers_mvp.Misc;
using checkers_mvp.ViewModels;
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

namespace checkers_mvp.Views
{
    /// <summary>
    /// Interaction logic for WinningStatsView.xaml
    /// </summary>
    public partial class WinningStatsView : Page
    {
        WinningStatsViewmodel WSvm;
        public WinningStatsView()
        {
            InitializeComponent();
            WSvm = new WinningStatsViewmodel();
            this.DataContext = WSvm;
        }

        public void Update()
        {
            WSvm.updateFromModel();
        }
    }
}
