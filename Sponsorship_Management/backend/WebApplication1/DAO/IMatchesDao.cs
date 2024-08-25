using SponsorShipManagement.Models;
//using WebApplication1.Models;
namespace SponsorShipManagement.Data
{
    public interface IMatchesDao
    {
        Task<List<Matches>> GetMatches();
        Task<List<q2>> Getq2();
        Task<List<q4>> Getq4(string year);


        Task<int> InsertPayment(q1 q);
    }
}
