using System;

namespace VOWsLauncher.MVVM.Model
{
    public class Template
    {
        public static readonly Template Empty = new Template("None", "", null, false);

        public string Name { get; }
        public string Description { get; }
        public Uri Location { get; }
        public bool IsWorkplace { get; }
        public string PageSize { get; }
        public string PageOrientation { get; }

        public Template(string name, string description, Uri location, bool isWorkplace)
        {
            Name = name;
            Description = description;
            Location = location;
            IsWorkplace = isWorkplace;
        }
    }
}
