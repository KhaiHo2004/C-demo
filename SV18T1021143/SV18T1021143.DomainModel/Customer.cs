using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021143.DomainModel
{
    
   public class Customer
    {
        /// <summary>
        /// Lấy ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContactName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public String City { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public String PostalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Country { get; set; }
        

    }
}
