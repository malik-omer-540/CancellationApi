namespace CancellationApi.Services
{
    public class LongTaskService : ILongTaskService
    {
        public async Task<string> RunLongTaskAsync(CancellationToken cancellationToken)
        {
            // Simulate work that respects cancellation
            for (int i = 0; i < 5; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000, cancellationToken); // 1 second steps
            }

            return "Long task completed successfully";
        }
    }
}
