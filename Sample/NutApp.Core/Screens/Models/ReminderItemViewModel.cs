using System;
using Nut.Core.Models;

namespace NutApp.Core.Screens.Models
{
    public class ReminderItemViewModel : NutViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Content { get; set; }
    }
}