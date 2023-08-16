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
        public string? PageSize { get; }
        public string? PageOrientation { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Template(string name, string description, Uri? location, bool isWorkplace)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Name = name;
            Description = description;
            if(location != null) Location = location;
            IsWorkplace = isWorkplace;
        }
    }
}
