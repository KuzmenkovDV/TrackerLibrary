using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private bool ValidateData()
        {
            //Default value == true, then check if something is wrong
            bool output = true;

            //Validate place number
            int placeNumber = 0;
            if (!int.TryParse(placeNumberValue.Text, out placeNumber))
            {
                output = false;
                throw new Exception("Invalid format");
            }

            if (placeNumber < 1)
            { 
                output = false;
                throw new Exception("Number value can not be less then 1");
            }

            //Validate place name
            if (placeNameValue.TextLength == 0)
            {
                throw new Exception("Place name can not be empty");
                output = false;
            }

            //Validate prizes

            decimal prizeAmount;
            int prizePercentage;
            
            if (!decimal.TryParse(prizeAmountValue.Text, out prizeAmount) || !int.TryParse(prizePecentageValue.Text, out prizePercentage))
            {

            }

            return output;

        }
    }
}
