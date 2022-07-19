using System.Windows.Forms;
using CheckersLogicLibrary;

namespace CheckersUI
{
    internal static class MainUI
    {
        internal static void RunGame()
        {
            GameSettingsWindow gameSettingsWindow = new GameSettingsWindow();
            GameAPI gameAPI;

            if (gameSettingsWindow.ShowDialog() != DialogResult.Cancel)
            {
                int gameBoardSize = gameSettingsWindow.SelectedBoardSize;
                string teamOneName = gameSettingsWindow.TeamOneName;
                bool isSinglePlayer = gameSettingsWindow.IsSinglePlayer;
                string teamTwoName = gameSettingsWindow.TeamTwoName;
                GameManager checkersGameManager = new GameManager(gameBoardSize, teamOneName, teamTwoName, isSinglePlayer);

                gameAPI = new GameAPI(checkersGameManager);
                gameAPI.StartGame();
            }
        }
    }
}
