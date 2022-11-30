using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextAccessSubmodul;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        //File names for all text storages
        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            //Load the text file, convert in PersonModel
            List<PersonModel> persons = PersonFile.FullFilePath().LoadFile().ConvertToPersonModels();

            //find the biggest id, make the new file;s id as id++

            int currentId = 1;
            if (persons.Count > 0)
                currentId = persons.OrderByDescending(p => p.Id).First().Id + 1;

            model.Id = currentId;
            currentId += 1;
            persons.Add(model);

            persons.SaveToPersonsFile(PersonFile);

            return model;
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            //Convert the text in PrizeModel
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //Find the biggest id, make the new file's id as id++
            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            currentId += 1;
            prizes.Add(model);

            //Convert the prizes to List<string>
            //Save the list to a text file
            prizes.SaveToPrizeFile(PrizesFile);


            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PersonFile);

            //Find the max id
            int currentId = 1;

            if (teams.Count > 0)
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile, PersonFile, PrizesFile);

            int currentId = 1;

            if (tournaments.Count > 0)
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id+1;

            model.Id = currentId;
            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);                       
        }

        public List<PersonModel> GetPerson_All()
        {
            return PersonFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeams_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PersonFile);
        }
    }
}
