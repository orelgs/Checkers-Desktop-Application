using System;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogicLibrary;

namespace CheckersUI
{
    internal partial class GameWindow : Form
    {
        private static readonly Size sr_GameBoardCellSize = new Size(75, 75);
        private static readonly Color sr_PreviousCellColor = Color.LightGoldenrodYellow;
        private static readonly Color sr_SelectedCellColor = Color.Aqua;
        private static readonly Color sr_UnselectedCellColor = Color.GhostWhite;
        private static readonly Color sr_DisabledCellColor = Color.DarkSlateGray;
        private readonly Point r_GameBoardTopLeft;

        public event Action<GameBoardCell> GameBoardCellSelected;

        public event Action PerformComputerMovement;

        private int GameBoardSize { get; set; }

        private PictureBox[,] GameBoardCellPictureBoxes { get; set; }
        
        private string TeamOneName { get; set; }

        private string TeamTwoName { get; set; }

        private int TeamOneArrowLeftLocation { get; set; }

        private int TeamTwoArrowLeftLocation { get; set; }

        public GameWindow(int i_GameBoardSize, string i_TeamOneName, string i_TeamTwoName, int i_TeamOneScore, int i_TeamTwoScore)
        {
            InitializeComponent();
            GameBoardSize = i_GameBoardSize;
            GameBoardCellPictureBoxes = new PictureBox[GameBoardSize, GameBoardSize];
            r_GameBoardTopLeft = new Point(sr_GameBoardCellSize.Width / 2, labelTeamOne.Bottom + labelTeamOne.Top);
            addGameBoardPictureBoxes(GameBoardSize);
            Width += sr_GameBoardCellSize.Width / 2;
            Height += sr_GameBoardCellSize.Height / 2;
            TeamOneName = i_TeamOneName;
            TeamTwoName = i_TeamTwoName;
            initializeTeamLabels(i_TeamOneScore, i_TeamTwoScore);
            TeamOneArrowLeftLocation = labelTeamOne.Left - pictureBoxTeamOneArrow.Width;
            TeamTwoArrowLeftLocation = labelTeamTwo.Left - pictureBoxTeamOneArrow.Width;
            pictureBoxTeamOneArrow.Left = TeamOneArrowLeftLocation;
            pictureBoxTeamTwoArrow.Left = TeamTwoArrowLeftLocation;
        }

        internal void UpdateTeamScores(int i_TeamOneScore, int i_TeamTwoScore)
        {
            labelTeamOne.Text = $"{TeamOneName}: {i_TeamOneScore}";
            labelTeamTwo.Text = $"{TeamTwoName}: {i_TeamTwoScore}";
        }

        private void initializeTeamLabels(int i_TeamOneScore, int i_TeamTwoScore)
        {
            UpdateTeamScores(i_TeamOneScore, i_TeamTwoScore);
            labelTeamTwo.Left = GameBoardCellPictureBoxes[0, GameBoardSize / 2].Right;
            labelTeamOne.Left = (labelTeamTwo.Left - (sr_GameBoardCellSize.Width * 2)) - labelTeamOne.Width;
        }

        private PictureBox createGameBoardPictureBoxCell(bool i_IsEnabled, Point i_CellLocation)
        {
            PictureBox createdPictureBoxCell = new PictureBox();

            createdPictureBoxCell.Location = i_CellLocation;
            createdPictureBoxCell.Size = sr_GameBoardCellSize;
            createdPictureBoxCell.TabStop = false;
            createdPictureBoxCell.Enabled = i_IsEnabled;
            createdPictureBoxCell.BorderStyle = BorderStyle.FixedSingle;
            createdPictureBoxCell.SizeMode = PictureBoxSizeMode.StretchImage;
            if (i_IsEnabled)
            {
                createdPictureBoxCell.Click += pictureBoxes_Click;
                createdPictureBoxCell.BackColor = sr_UnselectedCellColor;
            }
            else
            {
                createdPictureBoxCell.BackColor = sr_DisabledCellColor;
            }

            return createdPictureBoxCell;
        }

        private void addGameBoardPictureBoxes(int i_GameBoardSize)
        {
            Point currentPictureBoxLocation = r_GameBoardTopLeft;
            bool isCurrentPictureBoxEnabled = false;

            for (int i = 0; i < i_GameBoardSize; i++)
            {
                for (int j = 0; j < i_GameBoardSize; j++)
                {
                    GameBoardCellPictureBoxes[i, j] = createGameBoardPictureBoxCell(isCurrentPictureBoxEnabled, currentPictureBoxLocation);
                    Controls.Add(GameBoardCellPictureBoxes[i, j]);
                    isCurrentPictureBoxEnabled = !isCurrentPictureBoxEnabled;
                    currentPictureBoxLocation.X += sr_GameBoardCellSize.Width;                
                }

                isCurrentPictureBoxEnabled = !isCurrentPictureBoxEnabled;
                currentPictureBoxLocation.X = r_GameBoardTopLeft.X;
                currentPictureBoxLocation.Y += sr_GameBoardCellSize.Height;
            }
        }

        internal void UpdateCellOnGameBoard(GameBoardCell i_CellToUpdate, Image i_ImageToUpdate)
        {
            GameBoardCellPictureBoxes[i_CellToUpdate.Row, i_CellToUpdate.Column].Image = i_ImageToUpdate;
        }

        internal void SelectOrDeselectCell(GameBoardCell i_CellToUpdate, bool i_SelectCell)
        {
            if (i_SelectCell)
            {
                GameBoardCellPictureBoxes[i_CellToUpdate.Row, i_CellToUpdate.Column].BackColor = sr_SelectedCellColor;
            }
            else
            {
                GameBoardCellPictureBoxes[i_CellToUpdate.Row, i_CellToUpdate.Column].BackColor = sr_UnselectedCellColor;
            }
        }

        internal void HighlightTeamForCurrentTurn(eTeamType i_TeamForCurrentTurn)
        {
            switch (i_TeamForCurrentTurn)
            {
                case eTeamType.TeamOne:
                    pictureBoxTeamOneArrow.Visible = true;
                    pictureBoxTeamTwoArrow.Visible = false;
                    break;
                case eTeamType.TeamTwo:
                    pictureBoxTeamOneArrow.Visible = false;
                    pictureBoxTeamTwoArrow.Visible = true;
                    break;
            }
        }

        internal void HighlightOrRemoveHighlightFromPreviousTurnGameBoardCell(GameBoardCell i_GameBoardCellToHighlight, bool i_Highlight)
        {
            PictureBox cellPictureBox = GameBoardCellPictureBoxes[i_GameBoardCellToHighlight.Row, i_GameBoardCellToHighlight.Column];

            if (cellPictureBox.BackColor != sr_SelectedCellColor)
            {
                if (i_Highlight)
                {
                    cellPictureBox.BackColor = sr_PreviousCellColor;
                }
                else
                {
                    cellPictureBox.BackColor = sr_UnselectedCellColor;
                }
            }
        }

        internal void OnGameBoardCellSelected(GameBoardCell i_SelectedGameBoardCell)
        {
            GameBoardCellSelected?.Invoke(i_SelectedGameBoardCell);
        }

        private GameBoardCell calculateGameBoardCellOfPictureBox(PictureBox i_SelectedBoardPictureBox)
        {
            GameBoardCell cellOfSelectedPictureBox;
            int cellOfSelectedPictureBoxX, cellOfSelectedPictureBoxY;

            cellOfSelectedPictureBoxX = (i_SelectedBoardPictureBox.Left - r_GameBoardTopLeft.X) / sr_GameBoardCellSize.Width;
            cellOfSelectedPictureBoxY = (i_SelectedBoardPictureBox.Top - r_GameBoardTopLeft.Y) / sr_GameBoardCellSize.Height;
            cellOfSelectedPictureBox = new GameBoardCell(cellOfSelectedPictureBoxY, cellOfSelectedPictureBoxX);

            return cellOfSelectedPictureBox;
        }

        private void pictureBoxes_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            GameBoardCell cellOfClickedPictureBox = calculateGameBoardCellOfPictureBox(clickedPictureBox);

            ActiveControl = null;
            OnGameBoardCellSelected(cellOfClickedPictureBox);
        }

        private void timerComputerMovement_Tick(object sender, EventArgs e)
        {
            OnPerformComputerMovement();
        }

        internal void OnPerformComputerMovement()
        {
            PerformComputerMovement?.Invoke();
        }

        internal void StartOrStopComputerMovement(bool i_Start)
        {
            if (i_Start)
            {
                timerComputerMovement.Start();
            }
            else
            {
                timerComputerMovement.Stop();
            }
        }
    }
}
