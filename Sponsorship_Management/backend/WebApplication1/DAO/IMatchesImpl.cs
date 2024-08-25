using Npgsql;
using System.Data;
using SponsorShipManagement.Models;
//using WebApplication1.Models;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Diagnostics.Contracts;



namespace SponsorShipManagement.Data
{
    public class IMatchesImpl : IMatchesDao
    {
        NpgsqlConnection _connection;
        public IMatchesImpl(NpgsqlConnection connection)
        {
            _connection = connection;
        }


        // Develop an API endpoint that returns the details of all matches along with the  Match Date, Location and total amount of payments made for each match.
        public async Task<List<Matches>> GetMatches()
        {
            List<Matches> mlist = new List<Matches>();
            //string query = @"select * from spon_manag.matches";
            string query = @"select m.matchid ,m.matchname , m.matchdate , m.location , sum(p.amountpaid) as amt_paid  from spon_manag.matches m join spon_manag.contracts c on m.matchid = c.matchid join spon_manag.Payments p on c.contractid = p.contractid group by m.matchid;
";
            string errMessage = string.Empty;
            Matches m = null;

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
                        m = new Matches();
                        m.MatchID = reader.GetInt32(0);
                        m.MatchName = reader.GetString(1);
                        m.MatchDate = reader.GetDateTime(2);
                        m.Location = reader.GetString(3);
                        m.amt_paid = reader.GetInt32(4);
                        mlist.Add(m);
            
                    }
                }
                reader.Close();
            }
            catch (NpgsqlException e)
            {
                errMessage = e.Message;
                Console.WriteLine("------Exception-----:" + errMessage);
            }

            return mlist;
        }



        public async Task<List<q2>> Getq2()
        {
            List<q2> q2list = new List<q2>();
            //string query = @"select * from spon_manag.matches";
            string query = @"select s.sponsorid , s.industrytype , max(p.paymentdate) as latestpayment , count(p.paymentid) as numberofpay from spon_manag.sponsors s join spon_manag.contracts c on c.sponsorid = s.sponsorid join spon_manag.payments p on p.contractid = c.contractid group by s.sponsorid;
";
            string errMessage = string.Empty;
            q2 m = null;

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
                        m = new q2();
                        m.sponsorID = reader.GetInt32(0);
                        m.industrytype = reader.GetString(1);
                        m.latest_pay = reader.GetDateTime(2);
                        m.numberofpay = reader.GetInt32(3);
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


        public async Task<List<q4>> Getq4(string year)
        {
            List<q4> plist = new List<q4>();
            string query = @"select s.sponsorname , count(c.matchid) as numberOfspons from spon_manag.sponsors s join spon_manag.contracts c on s.sponsorid = c.sponsorid join spon_manag.payments p on c.contractid = p.contractid where p.paymentdate > DATE(@year || '-01-01') AND 
  p.paymentdate < DATE(@year || '-12-31') AND p.paymentstatus = 'Completed' group by s.sponsorid ";
            string errMessage = string.Empty;
            q4 p = null;

            try
            {
                await _connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@year", year);
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        p = new q4();
                        p.sponsorname = reader.GetString(0);
                        p.numberOfspons = reader.GetDouble(1);
                        Console.WriteLine("1");
                        plist.Add(p);
                    }
                }
                reader.Close();
            }
            catch (NpgsqlException e)
            {
                errMessage = e.Message;
                Console.WriteLine("------Exception-----:" + errMessage);
            }

            return plist;
        }


        public async Task<int> InsertPayment(q1 q)
        {
            int rowsInserted = 0;
            string message;

            
            string insertQuery = @$"INSERT INTO spon_manag.payments(contractid, PaymentDate, AmountPaid, PaymentStatus) SELECT {q.contractid}, '{q.paymentdate}', {q.amountpaid}, '{q.paymentstatus}' FROM spon_manag.contracts WHERE contractid = {q.contractid};";

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


    }
}
