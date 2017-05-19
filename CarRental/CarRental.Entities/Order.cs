namespace CarRental.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public int CarId { get; set; }

        public int CustomerId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime FinishDateTime { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Manager Manager { get; set; }
    }
}
