using NutApp.Core.Business;
using NutApp.Core.Storage;

namespace NutApp.Core.Repository
{
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(IEntityStorage entityStorage) : base(entityStorage)
        {
        }
    }
}