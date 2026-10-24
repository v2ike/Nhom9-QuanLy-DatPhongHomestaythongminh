using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlyvadatphonghomestay.Models
{
    public class TienNghi
    {
      
        public int Idtiennghi { get; set; }         
        public int HomestayId { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
    }
}
