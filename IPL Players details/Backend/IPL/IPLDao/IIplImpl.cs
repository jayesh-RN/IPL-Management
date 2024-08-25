using IPL.Models;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace IPL.IPLDao
{
    public class IIplImpl : IiplDao
    {
        NpgsqlConnection _connection;
        public IIplImpl(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> InsertPlayer(iplQ q)
        {
            int rowsInserted = 0;
            string message;


            string insertQuery = @$"INSERT INTO ipl.Players (player_name, team_id, role, age, matches_played) SELECT '{q.player_name}' , {q.team_id}, '{q.role}', {q.age}, {q.matches_played} FROM ipl.Teams WHERE team_id = {q.team_id};";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, _connection);
                    insertCommand.CommandType = CommandType.Text;
                    rowsInserted = await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
                Console.WriteLine("------Exception-----:" + message);
            }
            return rowsInserted;
        }



        public async Task<List<Q2>> Getq2()
        {
            List<Q2> q2list = new List<Q2>();
            string query = @"SELECT 
  M.match_id,
  T1.team_name AS team1_name,
  T2.team_name AS team2_name,
  M.venue,
  M.match_date,
  COUNT(FE.engagement_id) AS total_fan_engagements
FROM 
  ipl.Matches M
  JOIN ipl.Teams T1 ON M.team1_id = T1.team_id
  JOIN ipl.Teams T2 ON M.team2_id = T2.team_id
  LEFT JOIN ipl.Fan_Engagement FE ON M.match_id = FE.match_id
GROUP BY 
  M.match_id, T1.team_name, T2.team_name, M.venue, M.match_date
ORDER BY 
  M.match_id;";
            string errMessage = string.Empty;
            Q2 m = null;

            try
            {
                await _connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = new Q2();
                        m.match_id = reader.GetInt32(0);
                        m.team1_name = reader.GetString(1);
                        m.team2_name = reader.GetString(2);
                        m.venue = reader.GetString(3);
                        m.match_date = reader.GetDateTime(4);
                        m.total_fan_engagements = reader.GetInt64(5);
                        q2list.Add(m);
                    }
                }
                reader.Close();
            }
            catch (NpgsqlException e)
            {
                errMessage = e.Message;
                Console.WriteLine("------Exception-----:" + errMessage);
            }

            return q2list;
        }



        public async Task<List<Q3>> Getq3()
        {
            List<Q3> q3list = new List<Q3>();
            string query = @"SELECT 
  P.player_name, 
  COUNT(M.match_id) AS matches_played, 
  SUM(FE.engagement_id) AS total_fan_engagements
FROM 
  ipl.Players P
  JOIN ipl.Matches M ON P.team_id = M.team1_id OR P.team_id = M.team2_id
  JOIN ipl.Fan_Engagement FE ON M.match_id = FE.match_id
GROUP BY 
  P.player_name
ORDER BY 
  matches_played DESC, 
  total_fan_engagements DESC
LIMIT 5;";
            string errMessage = string.Empty;
            Q3 m = null;

            try
            {
                await _connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = new Q3();
                        m.player_name = reader.GetString(0);
                        m.matches_played = reader.GetInt32(1);
                        m.total_fan_engagements = reader.GetInt64(2);
                        q3list.Add(m);
                    }
                }
                reader.Close();
            }
            catch (NpgsqlException e)
            {
                errMessage = e.Message;
                Console.WriteLine("------Exception-----:" + errMessage);
            }

            return q3list;
        }



        //public async Task<List<Q4>> Getq4(DateTime startDate, DateTime endDate)
        public async Task<List<Q4>> Getq4(string startDate, string endDate)
        {
            List<Q4> q4list = new List<Q4>();
            string query = @"SELECT 
  M.match_id, 
  M.match_date, 
  M.venue, 
  T1.team_name AS team1_name, 
  T2.team_name AS team2_name
FROM 
  ipl.Matches M
  JOIN ipl.Teams T1 ON M.team1_id = T1.team_id
  JOIN ipl.Teams T2 ON M.team2_id = T2.team_id
WHERE 
  M.match_date BETWEEN @startDate AND @endDate
ORDER BY 
  M.match_date;";
            string errMessage = string.Empty;
            Q4 m = null;

            try
            {
                await _connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@startDate", NpgsqlDbType.Date).Value = DateTime.Parse(startDate);
                command.Parameters.Add("@endDate", NpgsqlDbType.Date).Value = DateTime.Parse(endDate);
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = new Q4();
                        m.match_id = reader.GetInt32(0);
                        m.match_date = reader.GetDateTime(1);
                        m.venue = reader.GetString(2);
                        m.team1_name = reader.GetString(3);
                        m.team2_name = reader.GetString(4);
                        q4list.Add(m);
                    }
                }
                reader.Close();
            }
            catch (NpgsqlException e)
            {
                errMessage = e.Message;
                Console.WriteLine("------Exception-----:" + errMessage);
            }

            return q4list;
        }




    }
}
