using Ensolvers.Note.Domain.Base;
using Ensolvers.Note.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain
{
    public class Tag : Entity
    {
        [ForeignKey("Note")]
        public string NoteId { get; private set; }
        public string Name { get; private set; }
        public TagColor Color { get; private set; }
        public TagColor BgColor { get; private set; }

        public Tag() : base() { }

        internal Tag(string name, int red, int green, int blue) : base()
        {
            Name = name;
            CreatedAt = DateTime.Now;
            Color = new TagColor(255, red, green, blue);
            BgColor = new TagColor(35, red, green, blue);
        }

        public override bool Compare(string value)
        {
            if(string.IsNullOrEmpty(value)) return false;
            return Name.ToLower().Contains(value);
        }
    }
}
