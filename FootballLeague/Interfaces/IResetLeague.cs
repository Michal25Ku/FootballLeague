namespace FootballLeagueLib.Interfaces
{
    public interface IResetLeague
    {
        /// <summary>
        /// Remove all data from Goals, Matches, Players and Clubs tables
        /// </summary>
        public void ResetDatabase();
    }
}