using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCreditApp2.Models
{
    public class Credit
    {
        // ID кредита
        [Required]
        public virtual int CreditId { get; set; }
        // Название
        [Required]
        [DisplayName("Название")]
        public virtual string Head { get; set; }
        // Период, на который выдается кредит
        [Required]
        [DisplayName("Период выдачи")]
        public virtual int Period { get; set; }
        // Максимальная сумма кредита
        [Required]
        [DisplayName("Максимальная сумма")]
        public virtual int Sum { get; set; }
        // Процентная ставка
        [Required]
        [DisplayName("Процентная ставка")]
        public virtual int Procent { get; set; }
    }
}