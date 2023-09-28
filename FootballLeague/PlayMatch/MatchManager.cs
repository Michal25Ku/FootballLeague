using FootballLeagueLib.Entities;
using Match = FootballLeagueLib.Entities.Match;

namespace FootballLeagueLib.PlayMatch
{
    /// <summary>
    /// This delegate responsibilities for called methods in the WPF application which changes a displayed match result
    /// </summary>
    /// <param name="minuteOfMatch">minute when a goal is scored</param>
    /// <param name="player">player who scored a goal</param>
    /// <param name="isHomeTeamShotGoal">flag which check is a home team(true) or an away team(false) scored a goal</param>
    public delegate void MatchResultChangedHandler(int minuteOfMatch, Player player, bool isHomeTeamShotGoal);

    /// <summary>
    /// This delegate responsibilities for changed a time of match in each minute
    /// </summary>
    public delegate void MatchTimeChangedHandler();

    /// <summary>
    /// This delegate responsibilities for the execution of all task in a WPF application related to match and table
    /// </summary>
    public delegate void MatchEndChangedHandler();

    /// <summary>
    /// This delegate responsibilities for starting the match 
    /// </summary>
    public delegate void MatchStartChangedHandler();
    public class MatchManager
    {
        #region events
        /// <summary>
        /// An event is called when a goal is scored
        /// </summary>
        public event MatchResultChangedHandler MatchResultChanged;

        /// <summary>
        /// An event is called in each minute of the match
        /// </summary>
        public event MatchTimeChangedHandler MatchTimeChanged;

        /// <summary>
        /// An event is called when match is gone
        /// </summary>
        public event MatchEndChangedHandler MatchEndChanged;

        /// <summary>
        /// An event is called when the match is started
        /// </summary>
        public event MatchStartChangedHandler MatchStartChanged;
        #endregion

        /// <summary>
        /// Match count 90 minutes, this is a fact
        /// </summary>
        public const int MATCH_TIME = 90;
        List<int> _playerWeightNumbers;

        /// <summary>
        /// The speed of simulation in real time
        /// TODO
        /// Bar which changes a simulation time
        /// </summary>
        public int SimulationTime { get; }
        public int MinuteInMatch { get; private set; }

        public Match PlayedMatch { get; private set; }

        /// <summary>
        /// This class has scored goal logic
        /// </summary>
        public MatchScoredGoal MatchScoreGoal { get; }
        public MatchPlayers MatchPlayers { get; }

        public List<Player> HomeTeamPlayers { get; private set; }
        public List<Player> AwayTeamPlayers { get; private set; }


        /// <summary>
        /// Constructor initializes the HomeTeamPlayers and the AwayTeamPlayers lists and sets the MinuteInMatch to 0
        /// </summary>
        /// <param name="playedMatch">The match during which all tasks related to its playing will be performed</param>
        public MatchManager(Match playedMatch)
        {
            MatchScoreGoal = new MatchScoredGoal();
            MatchPlayers = new MatchPlayers();
            PlayedMatch = playedMatch;

            HomeTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.HomeTeamId);
            AwayTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.AwayTeamId);

            //It creates the weight numbers list for the simulation random selection player
            _playerWeightNumbers = GenerateWeightedNumbers();

            SimulationTime = 0;
            MinuteInMatch = 0;
        }

        /// <summary>
        /// Starts the match and counts down time from 0 to 90 minutes.
        /// On start invokes the MatchStartChanged event.
        /// Initializes values for the match, sets the result to "0 - 0", and IsPlayed sets to true.
        /// It simules when and who score a goal.
        /// On the end, invokes the MatchEndChanged event
        /// </summary>
        /// <returns></returns>
        public async Task StartMatch()
        {
            MatchStartChanged?.Invoke();
            var rand = new Random();
            using var db = new FootballLeagueContext();

            db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch).IsPlayed = true;
            db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch).Result = PlayedMatch.GoalsHomeTeam + " - " + PlayedMatch.GoalsAwayTeam;

            for (int i = 1; i <= MATCH_TIME + 1; i++)
            {
                /// Each minute invokes this event to change the time in the match view
                MatchTimeChanged?.Invoke();

                // 1% chance to score goal
                if (rand.Next(100) <= 1)
                {
                    // each club has 50% chance to score the goal
                    int teamShoot = rand.Next(2);

                    if (teamShoot == 0)
                    {
                        // The chance to score a goal by a specific player depends on player position in the playground
                        int playerShoot = _playerWeightNumbers[rand.Next(_playerWeightNumbers.Count - 1)];

                        MatchScoreGoal.ScoreGoal(i, PlayedMatch.HomeTeamId, HomeTeamPlayers[playerShoot - 1].IdPlayer, this);
                        MatchResultChanged?.Invoke(i, HomeTeamPlayers[playerShoot - 1], true);
                    }
                    else
                    {
                        // The chance to score a goal by a specific player depends on player position in the playground
                        int playerShoot = _playerWeightNumbers[rand.Next(_playerWeightNumbers.Count - 1)];

                        MatchScoreGoal.ScoreGoal(i, PlayedMatch.AwayTeamId, AwayTeamPlayers[playerShoot - 1].IdPlayer, this);
                        MatchResultChanged?.Invoke(i, AwayTeamPlayers[playerShoot - 1], false);
                    }
                }

                MinuteInMatch = i;
                db.SaveChanges();
                
                await Task.Run(() => Thread.Sleep(5000 / 100));
            }

            await MatchEnd.UpdateAfterMatchIsOver(this);
            MatchEndChanged?.Invoke();
        }

        /// <summary>
        /// Generates numbers depends on player position to simulation which player is scored a goal
        /// </summary>
        /// <returns>List numbers like '1, 2, 2, 3, 3, 3, ...'</returns>
        public List<int> GenerateWeightedNumbers()
        {
            List<int> weightedNumbers = new List<int>();

            for (int i = 1; i <= 11; i++)
            {
                int weight = i;
                for (int j = 0; j < weight / 2; j++)
                {
                    weightedNumbers.Add(i);
                }
            }
            return weightedNumbers;
        }
    }
}
