using Ensolvers.Note.Domain.Base;

namespace Ensolvers.Note.Domain
{
    public class Note : Entity
    {
        public string Title { get; private set; }
        public string Body { get; private set; }
        public ICollection<Tag> Tags { get; private set; }

        internal Note() : base() { }

        public Note(string title, string body) : base()
        {
            Title = title;
            Body = body;
            Tags = new List<Tag>();
            CreatedAt = DateTime.Now;
        }

        public override bool Compare(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var valueLower = value.ToLower();
            if (Title.ToLower().Contains(valueLower)) return true;
            if (Body.ToLower().Contains(valueLower)) return true;
            var tag = Tags.FirstOrDefault(tag => tag.Compare(valueLower));
            if (tag is not null) return true;
            return false;
        }

        public Tag? AddTag(string name, int red, int green, int blue)
        {
            if (string.IsNullOrEmpty(name)) return default;
            var tag = new Tag(name, red, green, blue);
            Tags.Add(tag);
            return tag;
        }

        public void RemoveTag(Tag tag)
        {
            Tags.Remove(tag);
        }

        public void Update(string title, string body)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            if (!string.IsNullOrEmpty(body)) Body = body;
        }
    }
}