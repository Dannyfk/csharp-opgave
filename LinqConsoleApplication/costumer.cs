//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqConsoleApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class costumer
    {
        public costumer()
        {
            this.bookings = new HashSet<booking>();
        }
    
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    
        public virtual ICollection<booking> bookings { get; set; }
    }
}
