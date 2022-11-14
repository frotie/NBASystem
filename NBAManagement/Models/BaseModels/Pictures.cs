using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class Pictures
    {
        public int Id { get; set; }
        public byte[] Img { get; set; }
        public string Description { get; set; }
        public int NumberOfLike { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
