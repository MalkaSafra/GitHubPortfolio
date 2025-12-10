// Ensure you have the following NuGet package installed in your project:
// Microsoft.Extensions.Options

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options; // This requires the Microsoft.Extensions.Options NuGet package.
using Octokit;
using Portfolio.Services;

public class CachedGitHubService : IGitHubService
{
    private readonly IGitHubService _innerService; // The underlying GitHub service
    private readonly IMemoryCache _cache;
    private readonly GitHubClient _clientForEvents; // Separate client for event checking
    private readonly GitHubSettings _settings;

    private const string CacheKey = "PortfolioData";
    private const string LastUpdateKey = "PortfolioLastUpdate";

    public CachedGitHubService(IGitHubService innerService, IMemoryCache cache, IOptions<GitHubSettings> options)
    {
        _innerService = innerService;
        _cache = cache;
        _settings = options.Value;
        _clientForEvents = new GitHubClient(new ProductHeaderValue("CacheChecker"))
        {
            Credentials = new Credentials(_settings.Token)
        };
    }

    public async Task<List<PortfolioItem>> GetPortfolioAsync()
    {
        bool shouldRefresh = false;

        // 1. Check if data exists in cache
        if (!_cache.TryGetValue(CacheKey, out List<PortfolioItem> cachedData))
        {
            shouldRefresh = true;
        }
        else
        {
            // 2. Data exists, but is it up to date? Check GitHub for recent activity
            // Get the user's last event
            var events = await _clientForEvents.Activity.Events.GetAllUserPerformed(_settings.Username);
            var lastEvent = events.FirstOrDefault();

            if (lastEvent != null)
            {
                // Check when we last saved the data in cache
                var lastCacheTime = _cache.Get<DateTimeOffset?>(LastUpdateKey);

                // If the GitHub event occurred *after* the last cache save, refresh is needed
                if (lastCacheTime == null || lastEvent.CreatedAt > lastCacheTime)
                {
                    shouldRefresh = true;
                }
            }
        }

        if (shouldRefresh)
        {
            // Call the real service to fetch data
            cachedData = await _innerService.GetPortfolioAsync();

            // Save to cache
            _cache.Set(CacheKey, cachedData);
            _cache.Set(LastUpdateKey, DateTimeOffset.Now); // Record when we saved
        }

        return cachedData;
    }

    public async Task<List<PortfolioItem>> SearchRepositoriesAsync(string name, string language, string user)
    {
        // Search results are typically not cached
        return await _innerService.SearchRepositoriesAsync(name, language, user);
    }
}