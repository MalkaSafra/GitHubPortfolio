using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Microsoft.Extensions.Options;
namespace Portfolio.Services
{
    

    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubSettings _settings;

        public GitHubService(IOptions<GitHubSettings> options)
        {
            _settings = options.Value;
            _client = new GitHubClient(new ProductHeaderValue("MyPortfolioApp"));
            _client.Credentials = new Credentials(_settings.Token);
        }

        public async Task<List<PortfolioItem>> GetPortfolioAsync()
        {
            // Retrieve user repositories from settings
            var repos = await _client.Repository.GetAllForUser(_settings.Username);

            // Convert to portfolio items
            return repos.Select(repo => new PortfolioItem
            {
                Name = repo.Name,
                Language = repo.Language,
                Stars = repo.StargazersCount,
                LastCommit = repo.PushedAt, // PushedAt is a good indicator of recent activity
                Url = repo.HtmlUrl
            }).ToList();
        }

        public async Task<List<PortfolioItem>> SearchRepositoriesAsync(string name, string language, string user)
        {
            // Build search request
            var request = new SearchRepositoriesRequest(name)
            {
                User = user
            };

            if (!string.IsNullOrEmpty(language))
            {
                if (Enum.TryParse(language, true, out Language langEnum))
                {
                    request.Language = langEnum;
                }
            }

            var result = await _client.Search.SearchRepo(request);

            return result.Items.Select(repo => new PortfolioItem
            {
                Name = repo.Name,
                Language = repo.Language.ToString(),
                Stars = repo.StargazersCount,
                LastCommit = repo.PushedAt,
                Url = repo.HtmlUrl
            }).ToList();
        }
    }
}
