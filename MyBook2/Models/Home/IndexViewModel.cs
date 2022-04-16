using System.Collections.Generic;
using MyBook2.Services.Books.Models;

namespace MyBook2.Models.Home
{
    public class IndexViewModel
    {
        public int TotalBooks { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public IList<LatestBookServiceModel> Books { get; init; }
    }
}
