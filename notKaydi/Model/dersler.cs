using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notKaydi.Model
{
    public class dersler
    {
        [Key]
        public int dersID { get; set; }
        public string dersAdi { get; set; }
    }
}
