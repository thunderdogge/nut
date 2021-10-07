using System;

namespace NutApp.Core.Business
{
    public class Reminder : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}