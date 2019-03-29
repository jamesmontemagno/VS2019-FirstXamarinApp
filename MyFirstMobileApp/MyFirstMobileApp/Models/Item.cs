using Newtonsoft.Json;
using System;

namespace MyFirstMobileApp.Models
{
    public class Item
    {

        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        string first;
        [JsonIgnore]
        public string TextFirst
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Icon))
                    return string.Empty;
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