using System;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.entities.Sales
{
    public class Promotion
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Promotion? MainPromotion { get; set; }
        public ICollection<Product> PromotionalProducts { get; set; }
        
        public bool IsPromotionActive()
        {
            DateTime today = DateTime.Now;
            return (today >= StartDate) && (today <= EndDate);
        }
    }
}