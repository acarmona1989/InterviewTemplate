using System.Threading.Tasks;

namespace Football.Application.ExternalServices
{
    public interface IImproperAligmentNotificationService
    {
        Task NotifyImpromerAligmentAsync();
    }
}
