using GeoAPI.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Data.Models
{
    [Table("sl_stores")]
    public class Store
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        [Column("type_id")]
        public int? StoreTypeId { get; set; }

        [Column("lat")]
        public double? Latitude { get; set; }

        [Column("lng")]
        public double? Longitude { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("address1", TypeName = "varchar(255)")]
        public string Address1 { get; set; }

        [Column("address2", TypeName = "varchar(255)")]
        public string Address2 { get; set; }

        [Column("address3", TypeName = "varchar(255)")]
        public string Address3 { get; set; }

        [Column("city", TypeName = "varchar(100)")]
        public string City { get; set; }

        [Column("state", TypeName = "varchar(50)")]
        public string State { get; set; }

        [Column("postal_code", TypeName = "varchar(20)")]
        public string PostalCode { get; set; }

        [Column("country", TypeName = "varchar(50)")]
        public string Country { get; set; }

        [Column("phone", TypeName = "varchar(50)")]
        public string Phone { get; set; }

        [Column("booking_url", TypeName = "varchar(255)")]
        public string BookingUrl { get; set; }

        [Column("mon_hrs_o", TypeName = "varchar(20)")]
        public string MondayHoursOpen { get; set; }

        [Column("mon_hrs_c", TypeName = "varchar(20)")]
        public string MondayHoursClosed { get; set; }

        [Column("tue_hrs_o", TypeName = "varchar(20)")]
        public string TuesdayHoursClosed { get; set; }

        [Column("tue_hrs_c", TypeName = "varchar(20)")]
        public string TuesdayHoursOpen { get; set; }

        [Column("wed_hrs_o", TypeName = "varchar(20)")]
        public string WednesdayHoursClosed { get; set; }

        [Column("wed_hrs_c", TypeName = "varchar(20)")]
        public string WednesdayHoursOpen { get; set; }

        [Column("thu_hrs_o", TypeName = "varchar(20)")]
        public string ThursdayHoursClosed { get; set; }

        [Column("thu_hrs_c", TypeName = "varchar(20)")]
        public string ThursdayHoursOpen { get; set; }

        [Column("fri_hrs_o", TypeName = "varchar(20)")]
        public string FridayHoursClosed { get; set; }

        [Column("fri_hrs_c", TypeName = "varchar(20)")]
        public string FridayHoursOpen { get; set; }

        [Column("sat_hrs_o", TypeName = "varchar(20)")]
        public string SaturdayHoursClosed { get; set; }

        [Column("sat_hrs_c", TypeName = "varchar(20)")]
        public string SaturdayHoursOpen { get; set; }

        [Column("sun_hrs_o", TypeName = "varchar(20)")]
        public string SundayHoursOpen { get; set; }

        [Column("sun_hrs_c", TypeName = "varchar(20)")]
        public string SundayHoursClosed { get; set; }

        [Column("created_at", TypeName = "datetime2(0)")]
        public DateTime? Created { get; set; }

        [Column("updated_at", TypeName = "datetime2(0)")]
        public DateTime Updated { get; set; }

        [IgnoreDataMember]
        [Column("location")]
        public IPoint Location { get; set; }

        [ForeignKey(nameof(StoreTypeId))]
        public StoreType StoreType { get; set; }
    }
}
