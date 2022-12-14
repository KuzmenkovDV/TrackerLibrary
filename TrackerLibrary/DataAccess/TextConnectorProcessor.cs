using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextAccessSubmodul
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) //Prizemodel.csv
        {
            // E:\02_GIT\TournamentTracker\TextFilesFolder\PrizeModel.csv
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        public static List<string> LoadFile(this string fileToLoad)
        {
            if (!File.Exists(fileToLoad))
            {
                return new List<string>();
            }

            return File.ReadAllLines(fileToLoad).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = float.Parse(cols[4]);
                output.Add(p);

            }
            return output;
        }
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel()
                {

                    Id = int.Parse(cols[0]),
                    FirstName = cols[1],
                    LastName = cols[2],
                    EmailAdress = cols[3],
                    CellphoneNumber = cols[4]
                };
                output.Add(p);
            }
            return output;
        }
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel()
                {
                    Id = int.Parse(cols[0]),
                    TeamName = cols[1]
                };
                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());

                }
                output.Add(t);
            }

            return output;
        }
        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines,
            string teamFileName,
            string peopleFileName,
            string prizeFileName)
        {
            //id,TournamentName,EntryFee,(id|id|id - Entered Teams),(id|id|id - Prizes),(Rounds - id^id^id|id^id^id)
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamFileName
                .FullFilePath()
                .LoadFile()
                .ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizeFileName
                .FullFilePath()
                .LoadFile()
                .ConvertToPrizeModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TournamentModel tm = new TournamentModel()
                {
                    Id = int.Parse(cols[0]),
                    TournamentName = cols[1],
                    EntryFee = decimal.Parse(cols[2])
                };

                //Load teams by Ids
                string[] teamIds = cols[3].Split('|');
                foreach (string id in teamIds)
                {                   
                   tm.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                //Load prizes by Ids
                string[] prizesIds = cols[4].Split('|');
                foreach (string id in prizesIds)
                {
                    tm.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                //TODO - Rounds!

                output.Add(tm);                            
            }
            
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();


            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPersonsFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAdress},{p.CellphoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TeamModel t in models)
            {
                lines.Add($"{ t.Id },{ t.TeamName },{ ConvertPeopleListToString(t.TeamMembers) }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTournamentFile (this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TournamentModel tm in models)
            {
                lines.Add($"{ tm.Id }" +
                    $",{ tm.TournamentName }" +
                    $",{ tm.EntryFee }" +
                    $",{ ConvertTeamListToString(tm.EnteredTeams) }," +
                    $"{ ConvertPrizesListToString(tm.Prizes) }," +
                    $"{ ConvertRoundsListToString(tm.Rounds) }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertRoundsListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            if (rounds.Count == 0) return output;

            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ ConvertMatchupListToString(r)}|";
            }
            output.Remove(output.Length - 1);

            return output;
        }
        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";

            if (matchups.Count == 0) return output;

            foreach (MatchupModel matchup in matchups)
            {
                output += $"{matchup.Id}^";
            }
            output.Remove(output.Length - 1);

            return output;
        }
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            if (people.Count == 0) return output;

            foreach (PersonModel person in people)
            {
                output += $"{person.Id}|";
            }

            output.Remove(output.Length - 1);

            return output;
        }

        private static string ConvertTeamListToString(List<TeamModel> teams)
        {
            string output = "";

            if (teams.Count == 0) return output;

            foreach (TeamModel team in teams)
            {
                output += $"{ team.Id }|";
            }
            output.Remove(output.Length - 1);    

            return output;
        }

        private static string ConvertPrizesListToString (List<PrizeModel> prizes)
        {
            string output = "";

            if (prizes.Count == 0) return output;

            foreach (PrizeModel prize in prizes)
            {
                output += $"{ prize.Id }|";
            }
            output.Remove(output.Length - 1);

            return output;
        }
    }
}
