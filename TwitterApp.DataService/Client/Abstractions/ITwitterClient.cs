namespace TwitterApp.Client.Abstractions
{
    public interface ITwitterClient
    {
        public Task GetSampleStreamAsync(CancellationToken cancellationToken);
    }
}
