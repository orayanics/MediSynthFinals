using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("type", Schema = "record")]
    public class RecordType
    {
        [Key]
        public int rtypeId { get; set; }
        public string recordType { get; set; }
    }
}
