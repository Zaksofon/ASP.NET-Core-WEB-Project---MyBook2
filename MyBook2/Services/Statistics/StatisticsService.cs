using System.Linq;
using MyBook2.Data;

namespace MyBook2.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly MyBook2DbContext data;

        public StatisticsService(MyBook2DbContext data) => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalBooks = data.Books.Count();
            var totalUsers = data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalBooks = totalBooks,
                TotalUsers = totalUsers,
            };
        }
    }
}
