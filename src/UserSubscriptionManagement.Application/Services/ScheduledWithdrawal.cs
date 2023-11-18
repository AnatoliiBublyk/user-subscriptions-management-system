
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using UserSubscriptionManagement.Application.Services.Interfaces;

namespace UserSubscriptionManagement.Application.Services;

public class ScheduledWithdrawalService : IAsyncDisposable
{
    private Task _scheduledCall = null!;
    private CancellationTokenSource _cancellationTokenSource = null!;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _unsubscribeTimeDiff;
    private bool _isRunning;
    public ScheduledWithdrawalService(IServiceProvider serviceProvider, TimeSpan unsubscribeTimeDiff)
    {
        _serviceProvider = serviceProvider;
        _unsubscribeTimeDiff = unsubscribeTimeDiff;
    }

    public Task StartAsync()
    {
        if (_isRunning)
            return Task.CompletedTask;
        _isRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();
        _scheduledCall = Task.Run(() => ScheduleApiCall(_cancellationTokenSource.Token));
        return Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        if (!_isRunning)
            return;
        _isRunning = false;
        _cancellationTokenSource.Cancel();
        await _scheduledCall;
    }

    private async Task ScheduleApiCall(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var delay = GetDelay(_unsubscribeTimeDiff);
                //delay = TimeSpan.FromSeconds(10);
                Console.WriteLine("==============================================================");
                Console.WriteLine($"Automatic withdrawal will be executed at UTC: [{DateTime.UtcNow.Add(delay)}]");
                Console.WriteLine("==============================================================");
                await Task.Delay(delay, _cancellationTokenSource.Token);
                Console.WriteLine("==============================================================");
                Console.WriteLine($"[{DateTime.Now}] Executing automatic withdrawal");
                Console.WriteLine("==============================================================");
                //Run unsubscribe action
                using var scope = _serviceProvider.CreateScope();
                var userSubscriptionService = scope.ServiceProvider.GetRequiredService<IUserSubscriptionService>();
                await userSubscriptionService.PerformAutomaticWithdrawalAsync();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Scheduler was canceled");
            }
        }
    }

    private static DateTime GetDateTimeOfMonth(DateTime month, TimeSpan offset)
    {
        var startOfMonth = new DateTime(month.Year, month.Month, 1);
        DateTime result;
        if (offset >= TimeSpan.Zero)
        {
            result = startOfMonth.Add(offset);
        }
        else
        {
            var endOfMonth = startOfMonth.AddMonths(1);
            result = endOfMonth.Add(offset);
        }
        return result;
    }

    private static TimeSpan GetDelay(TimeSpan offset)
    {
        var currentDate = DateTime.UtcNow;
        var dateOfMonth = GetDateTimeOfMonth(currentDate, offset);

        var delay = dateOfMonth - currentDate;
        if (delay < TimeSpan.Zero)
        {
            dateOfMonth = GetDateTimeOfMonth(currentDate.AddMonths(1), offset);
            delay = dateOfMonth - currentDate;
        }
        return delay;
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync();
    }
}
