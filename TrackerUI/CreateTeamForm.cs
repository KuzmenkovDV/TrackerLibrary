using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form, ITeamRequester
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            //CreateSampleData();

            WireUpTheLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Dima", LastName = "Kuzmenkov" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Egor", LastName = "Pichugin" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Zoe", LastName = "Dechanell" });
        }
        private void WireUpTheLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateFormTeamMember())
            {
                PersonModel p = new PersonModel()
                {
                    FirstName = firstNameValue.Text,
                    LastName = lastNameValue.Text,
                    EmailAdress = emailValue.Text,
                    CellphoneNumber = cellphoneNumberValue.Text
                };

                GlobalConfig.Connection.CreatePerson(p);
                MessageBox.Show($"Person is created successfully");

                selectedTeamMembers.Add(p);
                WireUpTheLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneNumberValue.Text = "";

            }
            else
                MessageBox.Show("The data was incorrect. Values are not stored in database");
        }
        private bool ValidateFormTeamMember()
        {
            bool output = true;
            string errorMessage = "";

            if (firstNameValue.Text.Length == 0)
            {
                errorMessage += "Please fill the first name value\n";
                output = false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                errorMessage += "Please fill the first name value\n";
                output = false;
            }

            if (emailValue.Text.Length == 0)
            {
                errorMessage += "Please fill the first name value\n";
                output = false;
            }

            if (cellphoneNumberValue.Text.Length == 0)
            {
                errorMessage += "Please fill the cellphone number value";
                output = false;
            }

            if (!output)
                MessageBox.Show(errorMessage);


            return output;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpTheLists();
            }
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Add(p);
                selectedTeamMembers.Remove(p);

                WireUpTheLists();
            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            bool transactionAllowed = true;

            //Validate name
            if (teamNameValue.Text.Length > 0)
                t.TeamName = teamNameValue.Text;
            else
                transactionAllowed = false;

            //Validate list
            if (selectedTeamMembers.Count > 0)
                t.TeamMembers = selectedTeamMembers;
            else
                transactionAllowed = false;

            if (transactionAllowed)
            {
                t = GlobalConfig.Connection.CreateTeam(t);
                MessageBox.Show("Team successfully created");
                callingForm.TeamComplete(t);
                this.Close();
            }
            else
            {
                MessageBox.Show("Name is empty or/and no members are on the list");
            }

            
        }

        public void TeamComplete(TeamModel model)
        {
                      
        }
    }
}
