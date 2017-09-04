using System;

namespace LibraryProject.Models
{
    [Serializable]
    public class NewsPaper : PrintEdition
    {
        public string Category { get; set; }
    }
}