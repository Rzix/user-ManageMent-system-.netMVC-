using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Database_First.Models.MetaDataModels
{
    public class UserMetaData
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام نباید خالی باشد")]
        public string Name { get; set; }
        [Display(Name = "فامیل")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string Family { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "قالب ایمیل نا معتبر است")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "لطفا تعداد حروف بین 6 تا 12 حرف باشد")]
        public string Password { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]

        public string Address { get; set; }
        [Display(Name = "وضعیت تاهل")]
        public bool? Marital { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy}")]
        public DateTime RegisterDate { get; set; }
    }
}