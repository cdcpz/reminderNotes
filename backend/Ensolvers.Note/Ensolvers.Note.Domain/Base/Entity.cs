using Ensolvers.Note.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.Base
{
    public abstract class Entity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Entity()
        {
            GenId();
        }

        public void GenId()
        {
            if(string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }

        public abstract bool Compare(string value);
    }
}
