using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CheckersLogicLibrary;

namespace CheckersUI
{
    internal class GameAPI
    {
        private readonly Image r_TeamOnePawnGamePieceChar = global::CheckersUI.Properties.Resources.White_Pawn;
        private readonly Image r_TeamTwoPawnGamePieceChar = global::CheckersUI.Properties.Resources.Black_Pawn;
        private readonly Image r_TeamOneKingGamePieceChar = global::CheckersUI.Properties.Resources.White_King;
        private readonly Image r_TeamTwoKingGamePieceChar = global::CheckersUI.Properties.Resources.Black_King;
        private GameBoardCell? m_PreviousTurnSourceGameBoardCell = null;
        private GameBoardCell? m_PreviousTurnDestinationGameBoardCell = null;

        private GameBoardCell? CurrentSelectedGameBoardCell { get; set; }

        private List<GameBoardCell> AvailableMovesListForSelectedGamePiece { get; set; } = null;

        private bool DidSelectedGamePieceJustCapture { get; set; } = false;

        private GameManager CheckersGameManager { get; set; }

        private GameWindow CheckersGameWindow { get; set; }

        private int GameBoardSize { get; set; }

        internal GameAPI(GameManager io_CheckersGameManager)
        {
            CheckersGameManager = io_CheckersGameManager;
            GameBoardSize = CheckersGameManager.GameBoardSize;
            CheckersGameWindow = new GameWindow(GameBoardSize, CheckersGameManager.TeamOneName, CheckersGameManager.TeamTwoName, CheckersGameManager.TeamOneScore, CheckersGameManager.TeamTwoScore);
            CheckersGameWindow.GameBoardCellSelected += CheckersGameWindow_GameBoardCellSelected;
            if (CheckersGameManager.IsSinglePlayerGame)
            {
                CheckersGameWindow.PerformComputerMovement += CheckersGameWindow_PerformComputerMovement;
            }

            updateAllCellsOnGameBoard();
            CheckersGameWindow.HighlightTeamForCurrentTurn(CheckersGameManager.TeamForCurrentTurn);
        }

        private void displayErrorMessage(string i_MessageToDisplay)
        {
            MessageBox.Show(i_MessageToDisplay, "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal void CheckersGameWindow_PerformComputerMovement()
        {
            GameBoardCell previousTurnSourceGameBoardCell;
            GameBoardCell previousTurnDestinationGameBoardCell;

            CheckersGameManager.PerformComputerMovement(out previousTurnSourceGameBoardCell, out previousTurnDestinationGameBoardCell);
            updateAllCellsOnGameBoard();
            if (CheckersGameManager.TeamForCurrentTurn == eTeamType.TeamOne)
            {
                CheckersGameWindow.StartOrStopComputerMovement(false);
            }

            if (CheckersGameManager.GameState != eGameState.InProgress)
            {
                handlePreviousTurnHighlights(previousTurnSourceGameBoardCell, previousTurnDestinationGameBoardCell);
                showResultsAndAskForRematch();
            }
            else
            {
                handleTurnHighlights(previousTurnSourceGameBoardCell, previousTurnDestinationGameBoardCell);
            }
        }

        private void handleTurnHighlights(GameBoardCell i_PreviousTurnSourceGameBoardCell, GameBoardCell i_PreviousTurnDestinationGameBoardCell)
        {
            CheckersGameWindow.HighlightTeamForCurrentTurn(CheckersGameManager.TeamForCurrentTurn);
            handlePreviousTurnHighlights(i_PreviousTurnSourceGameBoardCell, i_PreviousTurnDestinationGameBoardCell);
        }

        private void handlePreviousTurnHighlights(GameBoardCell i_PreviousTurnSourceGameBoardCell, GameBoardCell i_PreviousTurnDestinationGameBoardCell)
        {
            if (m_PreviousTurnSourceGameBoardCell != null)
            {
                removePreviousTurnHighlights();
            }

            CheckersGameWindow.HighlightOrRemoveHighlightFromPreviousTurnGameBoardCell(i_PreviousTurnSourceGameBoardCell, true);
            CheckersGameWindow.HighlightOrRemoveHighlightFromPreviousTurnGameBoardCell(i_PreviousTurnDestinationGameBoardCell, true);
            m_PreviousTurnSourceGameBoardCell = i_PreviousTurnSourceGameBoardCell;
            m_PreviousTurnDestinationGameBoardCell = i_PreviousTurnDestinationGameBoardCell;
        }

        private void removePreviousTurnHighlights()
        {
            CheckersGameWindow.HighlightOrRemoveHighlightFromPreviousTurnGameBoardCell(m_PreviousTurnSourceGameBoardCell.Value, false);
            CheckersGameWindow.HighlightOrRemoveHighlightFromPreviousTurnGameBoardCell(m_PreviousTurnDestinationGameBoardCell.Value, false);
        }

        private void updateTeamScoresInGameWindow()
        {
            CheckersGameWindow.UpdateTeamScores(CheckersGameManager.TeamOneScore, CheckersGameManager.TeamTwoScore);
        }

        private Image getImageOfCellInGameBoard(GameBoardCell i_CellToPrint)
        {
            Image imageOfCell = null;

            if (CheckersGameManager.IsGameBoardCellOccupied(i_CellToPrint))
            {
                bool isGamePieceInBoardCellKing = CheckersGameManager.IsGamePieceInBoardCellKing(i_CellToPrint);

                if (CheckersGameManager.GetTeamAssignmentOfGamePieceInBoardCell(i_CellToPrint) == eTeamType.TeamOne)
                {
                    imageOfCell = r_TeamOnePawnGamePieceChar;
                    if (isGamePieceInBoardCellKing)
                    {
                        imageOfCell = r_TeamOneKingGamePieceChar;
                    }
                }
                else
                {
                    imageOfCell = r_TeamTwoPawnGamePieceChar;
                    if (isGamePieceInBoardCellKing)
                    {
                        imageOfCell = r_TeamTwoKingGamePieceChar;
                    }
                }
            }

            return imageOfCell;
        }

        private void updateAllCellsOnGameBoard()
        {
            for (int i = 0; i < GameBoardSize; i++)
            {
                for (int j = 0; j < GameBoardSize; j++)
                {
                    GameBoardCell currentGameBoardCell = new GameBoardCell(i, j);
                    Image gameBoardCellImage = getImageOfCellInGameBoard(currentGameBoardCell);

                    CheckersGameWindow.UpdateCellOnGameBoard(currentGameBoardCell, gameBoardCellImage);
                }
            }
        }

        private void selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(GameBoardCell i_CellToSelect, bool i_Select)
        {
            if (i_Select)
            {
                AvailableMovesListForSelectedGamePiece = CheckersGameManager.GetAvailableMovesListForGamePiece(i_CellToSelect);
                CheckersGameWindow.SelectOrDeselectCell(i_CellToSelect, true);
                CurrentSelectedGameBoardCell = i_CellToSelect;
                selectOrDeselectListOfCellsOnGameBoard(AvailableMovesListForSelectedGamePiece, true);
            }
            else
            {
                CheckersGameWindow.SelectOrDeselectCell(i_CellToSelect, false);
                CurrentSelectedGameBoardCell = null;
                selectOrDeselectListOfCellsOnGameBoard(AvailableMovesListForSelectedGamePiece, false);
                AvailableMovesListForSelectedGamePiece = null;
            }
        }

        private void selectOrDeselectListOfCellsOnGameBoard(List<GameBoardCell> i_ListOfCellToSelect, bool i_Select)
        {
            foreach (GameBoardCell cellToSelect in i_ListOfCellToSelect)
            {
                CheckersGameWindow.SelectOrDeselectCell(cellToSelect, i_Select);
            }
        }

        private void handleFirstSelectionOfGameBoardCell(GameBoardCell i_SelectedGameBoardCell)
        {
            if (CheckersGameManager.IsGameBoardCellOccupied(i_SelectedGameBoardCell))
            {
                if (CheckersGameManager.GetTeamAssignmentOfGamePieceInBoardCell(i_SelectedGameBoardCell) != CheckersGameManager.TeamForCurrentTurn)
                {
                    if (!CheckersGameManager.IsSinglePlayerGame || CheckersGameManager.GetTeamAssignmentOfGamePieceInBoardCell(i_SelectedGameBoardCell) == eTeamType.TeamTwo)
                    {
                        displayErrorMessage("You may only select game pieces of your own team!");
                    }
                }
                else
                {
                    if (!CheckersGameManager.IsSinglePlayerGame || CheckersGameManager.GetTeamAssignmentOfGamePieceInBoardCell(i_SelectedGameBoardCell) == eTeamType.TeamOne)
                    {
                        selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(i_SelectedGameBoardCell, true);
                    }
                }
            }
        }

        private void handleSecondSelectionOfGameBoardCell(GameBoardCell i_SelectedGameBoardCell)
        {
            if (i_SelectedGameBoardCell.Equals(CurrentSelectedGameBoardCell))
            {
                if (!DidSelectedGamePieceJustCapture)
                {
                    selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(i_SelectedGameBoardCell, false);
                }
                else
                {
                    displayErrorMessage("You must move with the selected game piece!");
                }
            }
            else if (CheckersGameManager.IsGameBoardCellOccupied(i_SelectedGameBoardCell) && CheckersGameManager.GetTeamAssignmentOfGamePieceInBoardCell(i_SelectedGameBoardCell) == CheckersGameManager.TeamForCurrentTurn)
            {
                if (!DidSelectedGamePieceJustCapture)
                {
                    selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(CurrentSelectedGameBoardCell.Value, false);
                    selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(i_SelectedGameBoardCell, true);
                }
                else
                {
                    displayErrorMessage("You must move with the selected game piece!");
                }
            }
            else
            {
                if (CheckersGameManager.IsAttemptedMovementValid(CurrentSelectedGameBoardCell.Value, i_SelectedGameBoardCell))
                {
                    handleMovement(CurrentSelectedGameBoardCell.Value, i_SelectedGameBoardCell);
                }
                else
                {
                    displayErrorMessage("You cannot move there!");
                }
            }
        }

        private void showResultsAndAskForRematch()
        {
            StringBuilder resultsMessage = new StringBuilder();
            DialogResult rematchOptionSelected;

            switch (CheckersGameManager.GameState)
            {
                case eGameState.TeamOneWon:
                    resultsMessage.AppendLine($"{CheckersGameManager.TeamOneName} won!");
                    break;
                case eGameState.TeamTwoWon:
                    resultsMessage.AppendLine($"{CheckersGameManager.TeamTwoName} won!");
                    break;
                case eGameState.Tie:
                    resultsMessage.AppendLine("Tie!");
                    break;
            }

            resultsMessage.AppendLine("Another Round?");
            rematchOptionSelected = MessageBox.Show(resultsMessage.ToString(), "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            handleRematchSelection(rematchOptionSelected);
        }

        private void handleRematchSelection(DialogResult i_RematchOptionSelected)
        {
            switch (i_RematchOptionSelected)
            {
                case DialogResult.Yes:
                    CheckersGameManager.PrepareGameForRematch();
                    updateAllCellsOnGameBoard();
                    updateTeamScoresInGameWindow();
                    removePreviousTurnHighlights();
                    CheckersGameWindow.HighlightTeamForCurrentTurn(CheckersGameManager.TeamForCurrentTurn);
                    m_PreviousTurnDestinationGameBoardCell = null;
                    m_PreviousTurnSourceGameBoardCell = null;
                    break;
                case DialogResult.No:
                    CheckersGameWindow.Close();
                    break;
            }
        }

        private void handleMovement(GameBoardCell i_SourceGameBoardCell, GameBoardCell i_DestinationGameBoardCell)
        {
            eTeamType teamForCurrentTurn = CheckersGameManager.TeamForCurrentTurn;

            CheckersGameManager.MoveGamePiece(i_SourceGameBoardCell, i_DestinationGameBoardCell);
            selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(i_SourceGameBoardCell, false);
            updateAllCellsOnGameBoard();
            if (CheckersGameManager.GameState == eGameState.InProgress)
            {
                handleTurnHighlights(i_SourceGameBoardCell, i_DestinationGameBoardCell);
                if (CheckersGameManager.TeamForCurrentTurn == teamForCurrentTurn)
                {
                    selectOrDeselectGamePieceAndAvailableMovesOnGameBoard(i_DestinationGameBoardCell, true);
                    DidSelectedGamePieceJustCapture = true;
                }
                else
                {
                    DidSelectedGamePieceJustCapture = false;
                    if (CheckersGameManager.IsSinglePlayerGame)
                    {
                        CheckersGameWindow.StartOrStopComputerMovement(true);
                    }
                }
            }
            else
            {
                handlePreviousTurnHighlights(i_SourceGameBoardCell, i_DestinationGameBoardCell);
            }
        }

        internal void CheckersGameWindow_GameBoardCellSelected(GameBoardCell i_SelectedGameBoardCell)
        {
            if (CurrentSelectedGameBoardCell == null)
            {
                handleFirstSelectionOfGameBoardCell(i_SelectedGameBoardCell);
            }
            else
            {
                handleSecondSelectionOfGameBoardCell(i_SelectedGameBoardCell);

                if (CheckersGameManager.GameState != eGameState.InProgress)
                {
                    showResultsAndAskForRematch();
                }
            }
        }

        public void StartGame()
        {
            CheckersGameWindow.ShowDialog();
        }
    }
}
