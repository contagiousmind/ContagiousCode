using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContagiousCode.Web
{
    public class Ajax
    {
        // CHECK 2 3
        public object Data { get; set; }
        public int ItemID { get; set; }
        public object Json { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }

        public Ajax()
        {
            this.Json = "";
            this.Data = "";
            this.Message = "";
            this.ItemID = 0;
            this.Status = 0;
        }


    }
}
