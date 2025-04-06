using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class Date
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2(0)")]
        public DateTime AppoinmentDate { get; set; }


    }
}