//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoadTex.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerAddress
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AdressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}