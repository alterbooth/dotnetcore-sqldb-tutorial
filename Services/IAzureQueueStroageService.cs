using System.Threading.Tasks;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Services
{
    public interface IAzureQueueStroageService
    {
        Task Send(Todo todo);
    }
}