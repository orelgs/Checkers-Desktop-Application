using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CheckersUI
{
    internal partial class GameSettingsWindow : Form
    {
        public GameSettingsWindow()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = (sender as CheckBox).Checked;
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Text = "Computer";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (handleAndReturnTeamNameValidity())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool handleAndReturnTeamNameValidity()
        {
            bool areTeamNamesValidResult = true;

            if (!isValidTeamName(textBoxPlayer1))
            {
                errorProviderInvalidTeamName.SetError(textBoxPlayer1, "Team name must be 1-20 characters!");
                areTeamNamesValidResult = false;
            }

            if (!isValidTeamName(textBoxPlayer2))
            {
                errorProviderInvalidTeamName.SetError(textBoxPlayer2, "Team name must be 1-20 characters!");
                areTeamNamesValidResult = false;
            }

            return areTeamNamesValidResult;
        }

        private bool isValidTeamName(TextBox i_TeamNameTextBox)
        {
            bool isStringValidTeamNameResult;
            string teamName = i_TeamNameTextBox.Text;

            isStringValidTeamNameResult = !string.IsNullOrWhiteSpace(teamName) && teamName.Length >= 1 && teamName.Length <= 20 && Regex.IsMatch(teamName, @"^[a-zA-Z0-9 ]+$");

            return isStringValidTeamNameResult;
        }

        internal int SelectedBoardSize
        {
            get
            {
                int selectedBoardSize;

                if (radioButton6X6.Checked)
                {
                    selectedBoardSize = 6;
                }
                else if (radioButton8X8.Checked)
                {
                    selectedBoardSize = 8;
                }
                else
                {
                    selectedBoardSize = 10;
                }

                return selectedBoardSize;
            }
        }

        internal string TeamOneName
        {
            get
            {
                return textBoxPlayer1.Text;
            }
        }

        internal string TeamTwoName
        {
            get
            {
                return textBoxPlayer2.Text;
            }
        }

        internal bool IsSinglePlayer
        {
            get
            {
                return !checkBoxPlayer2.Checked;
            }
        }
    }
}
