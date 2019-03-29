using System;

namespace MyFirstMobileApp.Models
{
    public class Item
    {

        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        string first;
        public string TextFirst
        {
            get
            {
                if(first == null)
                {
                    first = Text?.Substring(0, 1) ?? string.Empty;
                }
                return first;
            }
            set { first = value; }
        }
    }
}