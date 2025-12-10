using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public interface IGitHubService
    {
        Task<List<PortfolioItem>> GetPortfolioAsync();
        Task<List<PortfolioItem>> SearchRepositoriesAsync(string name, string language, string user);
    }
}
