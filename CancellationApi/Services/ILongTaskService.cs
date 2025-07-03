namespace CancellationApi.Services
{
    public interface ILongTaskService
    {
        Task<string> RunLongTaskAsync(CancellationToken cancellationToken);
    }
}
