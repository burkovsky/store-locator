using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("sl_types")]
    public class StoreType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column("weight")]
        public short? Weight { get; set; }

        [Column("created_at", TypeName = "datetime2(0)")]
        public DateTime? Created { get; set; }

        [Column("updated_at", TypeName = "datetime2(0)")]
        public DateTime Updated { get; set; }
    }
}
