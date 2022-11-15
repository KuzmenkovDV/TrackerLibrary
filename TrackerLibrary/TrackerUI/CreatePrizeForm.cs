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

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {            
            if (ValidateData())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);
                foreach (IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }
                placeNumberValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";
            }
            else
            {
                MessageBox.Show("This form has invalid information. Place check it and try again");
            }
        }

        private bool ValidateData()
        {
            //Default value == true, then check if something is wrong
            bool output = true;
            string errorMessage = "";

            //Validate place number
            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);
            if (!placeNumberValidNumber)
            {
                output = false;
                errorMessage += "Invalid format of number value/n";
            }

            if (placeNumber < 1)
            { 
                output = false;
                errorMessage+="Number value can not be less then 1/n";
            }

            //Validate place name
            if (placeNameValue.TextLength == 0)
            {
                output = false;
                errorMessage="Place name can not be empty/n";                
            }

            //Validate prizes

            decimal prizeAmount = 0;
            double prizePercentage = 0;
            bool prizeAmountValidNumber = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentageValidNumber = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (!prizeAmountValidNumber && !prizePercentageValidNumber)
            {
                output=false;
                errorMessage=($"Invalid data. You have entered prize amount as {prizeAmount} and prize percentage as {prizePercentage}. Input one of them correctly/n");
            }

            if ((prizeAmount <= 0 && prizePercentage <= 0) || prizePercentage > 100)
            {
                output = false;
                errorMessage+=("Amount can not be equal or less then 0. Percentage shoud be between 0 and 100");
            }


            if (!output)
                throw new Exception(errorMessage);
            return output;

        }

        private void prizeAmountValue_TextChanged(object sender, EventArgs e)
        {
            if (prizeAmountValue.Text.Length != 0 && prizeAmountValue.Text !="0")
            { 
                prizePercentageValue.Text = "0";
                prizePercentageValue.Enabled = false;
            }
            else
            {
                prizeAmountValue.Text = "0";
                prizePercentageValue.Enabled = true;
            }
        }

        private void prizePercentageValue_TextChanged(object sender, EventArgs e)
        {
            if (prizePercentageValue.Text.Length != 0 && prizePercentageValue.Text != "0")
            {
                prizeAmountValue.Text = "0";
                prizeAmountValue.Enabled = false;
            }
            else
            {
                prizePercentageValue.Text = "0";
                prizeAmountValue.Enabled = true;
            }
        }
    }
}
