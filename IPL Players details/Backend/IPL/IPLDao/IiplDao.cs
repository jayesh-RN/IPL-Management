using IPL.Models;

namespace IPL.IPLDao
{
    public interface IiplDao
    {
        Task<int> InsertPlayer(iplQ q);

        Task<List<Q2>> Getq2();
        Task<List<Q3>> Getq3();
        Task<List<Q4>> Getq4(string startDate, string endDate);
    }
}
