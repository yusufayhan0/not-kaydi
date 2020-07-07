using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notKaydi.Model
{
    public class nots
    {
        [Key]
        public int notID { get; set; }
        public int dersID { get; set; }
        public int ogrenciID { get; set; }
        public int vize1 { get; set; }
        public int vize2 { get; set; }
        public int vize3 { get; set; }
        public int final { get; set; }
    }
}
