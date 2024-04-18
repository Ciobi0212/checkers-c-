using checkers_mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace checkers_mvp.Views
{
    public partial class GameView : Page
    {
       public GameViewModel gameViewModel;
        public GameView()
        {
            InitializeComponent();
            gameViewModel = new GameViewModel();
            this.DataContext = gameViewModel;
         
        }

        private void BoardGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int row = (int)e.GetPosition(BoardGrid).Y / ((int)BoardGrid.Height / 8);
            int column = (int)e.GetPosition(BoardGrid).X / ((int)BoardGrid.Width / 8);

            gameViewModel.processClick(row, column);
        }

        public void saveGame()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            
            string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            fileDialog.FileName = "CheckersSave" + time;
            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "JSON files (.json)|*.json";

            DialogResult result = fileDialog.ShowDialog();

            string path = fileDialog.InitialDirectory + fileDialog.FileName;

            if (result == DialogResult.OK)
            {
                gameViewModel.saveGame(path);
            }
        }

        public void loadGame()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "JSON files (.json)|*.json";
            fileDialog.RestoreDirectory = true;

            DialogResult result = fileDialog.ShowDialog();

            string path = fileDialog.InitialDirectory + fileDialog.FileName;

            if (result == DialogResult.OK)
            {
                gameViewModel.loadGame(path);
            }
        }

        //public void newGame()
        //{
        //    gameViewModel.newGame();
        //}
    }
}
