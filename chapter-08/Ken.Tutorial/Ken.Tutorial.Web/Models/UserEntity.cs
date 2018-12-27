using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ken.Tutorial.Web.Models
{
    [Table("user")]
    [Serializable]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("hobby")]
        public string Hobby { get; set; }
    }
}