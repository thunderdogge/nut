using System;
using System.Linq;
using Nut.Core.Bindings.Commands;
using NutApp.Core.Business;
using NutApp.Core.Extensions;
using NutApp.Core.Repository;

namespace NutApp.Core.Screens.Models
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IReminderRepository reminderRepository;
        private ReminderItemViewModel[] items;

        public DashboardViewModel(IReminderRepository reminderRepository)
        {
            this.reminderRepository = reminderRepository;
        }

        public override void Resume()
        {
            base.Resume();

            Items = reminderRepository.GetAll().OrderByDescending(x => x.Date).Select(CreateItemViewModel).ToArray();
        }

        public INutCommand ItemAddCommand => new NutCommand(() => ShowViewModel<ReminderModifyViewModel>(Guid.Empty));

        public INutCommand ItemSelectCommand => new NutCommand<ReminderItemViewModel>(ItemSelectHandler);

        public ReminderItemViewModel[] Items
        {
            get { return items ?? (items = new ReminderItemViewModel[0]); }
            set { items = value; IsEmpty = value.IsNullOrEmpty(); RaisePropertyChanged(); }
        }

        private void ItemSelectHandler(ReminderItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            if (item.Id == Guid.Empty)
            {
                return;
            }

            ShowViewModel<ReminderModifyViewModel>(item.Id);
        }

        private static ReminderItemViewModel CreateItemViewModel(Reminder item)
        {
            return new ReminderItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                Date = item.Date.ToLocalTime().ToString("d MMMM HH:mm")
            };
        }
    }
}