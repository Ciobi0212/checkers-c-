using checkers_mvp.Commands;
using checkers_mvp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace checkers_mvp.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
       private Page page;

       private GameView gameView;

       private Home home;

       private WinningStatsView WSV;

       public int BoardsHeight { get {return (int)gameView.BoardGrid.Height; } set { gameView.BoardGrid.Height = value; } }
       public int BoardsWidth { get { return (int)gameView.BoardGrid.Width; } set { gameView.BoardGrid.Width = value; } }
       public int HomeHeight { get { return (int)home.homeGrid.Height; } set { home.homeGrid.Height = value; } }
       public int HomeWidth { get { return (int)home.homeGrid.Width; } set { home.homeGrid.Width = value; } }
       public int GameInfoWidth { get { return (int)gameView.gameInfo.Width; } set { gameView.gameInfo.Width = value; } }
       
       public bool isAMJToggled = false;
       public Page Page
        {
            get { return page; }
            set
            {
                page = value;
                OnPropertyChanged("Page");
            }
        }
        
        public ICommand HomeCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand SaveGameCommand { get; }
        public ICommand MenuCommand { get; }
        public ICommand AMJCommand { get; }
        public ICommand StatsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            home = new Home();
            gameView = new GameView();
            WSV = new WinningStatsView();

            Page = home;

            NewGameCommand = new RelayCommand(newGame, (object obj) => { return true; });
            LoadGameCommand = new RelayCommand(loadGame, (object obj) => { return true; });
            SaveGameCommand = new RelayCommand(saveGame, (object obj) => { return true; });
            HomeCommand = new RelayCommand(goHome, (object obj) => { return true; });
            MenuCommand = new RelayCommand(menuClick, (object obj) => { return true; });
            AMJCommand = new RelayCommand(AMJClick, (object obj) => { return true; });
            StatsCommand = new RelayCommand(StatsClick, (object obj) => { return true; });

            BoardsHeight = 750;
            BoardsWidth = 800;

            HomeHeight = 800;
            HomeWidth = 800;

            GameInfoWidth = 920;
        }

        private bool isMenuExpanded = true;

        public void shrinkMenu(Storyboard storyboardShrink)
        {
            storyboardShrink.Begin();
            isMenuExpanded = false;
            BoardsWidth = 920;
            BoardsHeight = 750;
            gameView.gameInfo.Visibility = Visibility.Visible;
        }

        public void expandMenu(Storyboard storyboardExpand)
        {
            BoardsWidth = 800;
            BoardsHeight = 850;

            gameView.gameInfo.Visibility = Visibility.Collapsed;

            storyboardExpand.Begin();

            isMenuExpanded = true;
        }

        public void StatsClick(object obj)
        {
            WSV.Update();
            Page = WSV;
        }
        public void menuClick(object obj)
        {
            if(obj is CommandParams commandParams)
            {
                var storyboardShrink = commandParams.storyboardShrink;
                var storyboardExpand = commandParams.storyboardExpand;

                if (isMenuExpanded)
                {
                    shrinkMenu(storyboardShrink);  
                }
                else
                {
                    expandMenu(storyboardExpand);
                }
            }
        }

        public void AMJClick(object obj)
        {
            isAMJToggled = !isAMJToggled;
        }

        public void goHome(object obj)
        {
            Page = home;
        }   

        public void saveGame(object obj)
        {
            if(Page != gameView)
            {
               MessageBox.Show("No game to save");
               return;
            }

            gameView.saveGame();
        }

        public void loadGame(object obj)
        {
            gameView.loadGame();
            Page = gameView;

            if (isMenuExpanded)
            { 
              shrinkMenu(obj as Storyboard);
            }
        }

        public void newGame(object obj)
        {
            gameView.gameViewModel.newGame(isAMJToggled);
            Page = gameView;

            if (isMenuExpanded)
            {
                shrinkMenu(obj as Storyboard);
            }
        }
    }
}
