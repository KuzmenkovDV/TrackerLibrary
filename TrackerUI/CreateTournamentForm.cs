using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeams_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();

        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpTheLists();
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //Create tournament model
            

            if (TournamentInformationIsValid())
            {
                //Create all of the prizes entries
                //Create all of the team entries
                //Create the matchups
                //TODO - wire up the matchups!
                TournamentModel tm = new TournamentModel()
                {
                    TournamentName = tournamentNameValue.Text,
                    EntryFee = Decimal.Parse(entryFeeValue.Text),  
                    Prizes = selectedPrizes,
                    EnteredTeams = selectedTeams
                };
                GlobalConfig.Connection.CreateTournament(tm);
                
            }
            else
                MessageBox.Show("The entered information was not valid. Transaction can not be done");                                    
        }


        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpTheLists();
            }
        }

        private void WireUpTheLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentPlayersListBox.DataSource = null;
            tournamentPlayersListBox.DataSource = selectedTeams;
            tournamentPlayersListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void removeSelectedTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentPlayersListBox.SelectedItem;

            if (t != null)
            {
                availableTeams.Add(t);
                selectedTeams.Remove(t);

                WireUpTheLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the create prize form
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            //Get back from the form a prizeModel
            //Take the PrizeModel and put it in the selectedPrize

            selectedPrizes.Add(model);
            WireUpTheLists();

        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpTheLists();
        }

        private void createTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);

                WireUpTheLists();
            }

        }

        private bool TournamentInformationIsValid()
        {
            bool output = true;
            string errorMessage = "";
            decimal entryFee = 0;


            if (tournamentNameValue.Text.Length == 0)
            {
                output = false;
                errorMessage += "Tournament name could not be empty\n";
            }

            //Check entry fee
            bool entryFeeIsValid = decimal.TryParse(entryFeeValue.Text, out entryFee);
            if ((!entryFeeIsValid)||(entryFee<0))
            {
                output = false;
                errorMessage += "Please enter the correct value for entry fee\n";
            }


            //Validate prizes and teams
            if (selectedPrizes.Count == 0)
            {
                output = false;
                errorMessage += "Please select prizes\n";
            }
            if (selectedTeams.Count == 0)
            {
                output = false;
                errorMessage += "Please select teams\n";
            }


            if (!output)
                MessageBox.Show(errorMessage);

            return output;

        }
    }
}
