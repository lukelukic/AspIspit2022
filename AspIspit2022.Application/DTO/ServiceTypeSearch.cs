using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Application.DTO
{
    public class ServiceSchedulesSearch
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public string Keyword { get; set; }
        public bool? IsFinished { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
