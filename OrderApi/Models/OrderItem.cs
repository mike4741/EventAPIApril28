using OrderApi.Infrastructure;
using OrderApi.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EventName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }

        public int  Units { get; set; }
        public int EventId { get; private set; }
        public OrderItem( int eventId , string eventName , decimal unitPrice , string pictureUrl , int units = 1)
        {
          if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of Units");
            }
            EventId = eventId;

            EventName = eventName;
            UnitPrice = unitPrice;

            Units = units;
            PictureUrl = pictureUrl;
        }
        public void SetPictureUri(string pictureUrl)
        {
            if (!string.IsNullOrWhiteSpace(pictureUrl))
            {
                PictureUrl = pictureUrl;
            }
        }
         public void AddUnits( int units)
        {
            if (Units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }
            units += units;
        }

    }
}
