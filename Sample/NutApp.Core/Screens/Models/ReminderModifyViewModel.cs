using System;
using Nut.Core.Bindings.Commands;
using Nut.Core.Models.Validation;
using NutApp.Core.Business;
using NutApp.Core.Components;
using NutApp.Core.Repository;

namespace NutApp.Core.Screens.Models
{
    public class ReminderModifyViewModel : BaseViewModel
    {
        private readonly INutValidator validator;
        private readonly IDialogFactory dialogFactory;
        private readonly IReminderRepository reminderRepository;
        private string titleError;

        public ReminderModifyViewModel(INutValidator validator,
                                       IDialogFactory dialogFactory,
                                       IReminderRepository reminderRepository)
        {
            this.validator = validator;
            this.dialogFactory = dialogFactory;
            this.reminderRepository = reminderRepository;
        }

        public void Init(Guid guid)
        {
            Id = guid;

            validator.For(this, x => x.Title, x => x.TitleError).Required("Title is required");
        }

        public override void Start()
        {
            base.Start();

            var item = reminderRepository.Get(Id);
            if (item != null)
            {
                Title = item.Title;
                Content = item.Content;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string TitleError
        {
            get { return titleError; }
            set { titleError = value; RaisePropertyChanged(); }
        }

        public string Content { get; set; }

        public INutCommand SaveCommand => new NutCommand(SaveHandler);

        public INutCommand DeleteCommand => new NutCommand(DeleteHandler);

        private void SaveHandler()
        {
            var validation = validator.Validate();
            if (validation.IsInvalid)
            {
                return;
            }

            var title = (Title ?? string.Empty).Trim();
            var content = (Content ?? string.Empty).Trim();
            var entity = new Reminder
            {
                Id = Id == Guid.Empty ? Guid.NewGuid() : Id,
                Title = title,
                Content = content,
                Date = DateTime.UtcNow
            };

            reminderRepository.Write(entity);
            CloseViewModel(this);
        }

        private void DeleteHandler()
        {
            var dialog = dialogFactory.CreateConfirm();
            dialog.Show("Are you sure?", DeletePositiveHandler);
        }

        private void DeletePositiveHandler()
        {
            reminderRepository.Delete(Id);
            CloseViewModel(this);
        }
    }
}