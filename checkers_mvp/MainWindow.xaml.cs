using checkers_mvp.Views;
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
using Newtonsoft.Json;
using checkers_mvp.Commands;
using checkers_mvp.ViewModels;
using System.Windows.Media.Animation;

namespace checkers_mvp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       MainViewModel mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;
            
            CommandParams commandParams = new CommandParams((Storyboard)FindResource("SlideOutFromRight"), (Storyboard)FindResource("SlideInFromRight"));
            btnMenu.CommandParameter = commandParams;

            this.ResizeMode = ResizeMode.NoResize;
        }
    }
}
