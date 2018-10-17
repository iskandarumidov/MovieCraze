using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MvcMovie.Common.Validation;

namespace MvcMovie.Models
{
    public class ContactModel : ContactModelBase
    {
        public int ContactModelId { get; set; }
        //public int ClientId { get; set; }
        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [ConsoleAndGameCheck]
        public string Console { get; set; }
        [Required]
        public string Game { get; set; }

        [NotMapped]
        [AgreedToTermsCheck]
        [Required]
        public bool AgreedToTerms { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        //[Range(1, 17)]
        [EnteredProperAge]
        public int? Age { get; set; }
    }

    //using System;
    //using System.Collections.Generic;
    //using System.Web;
    //using System.ComponentModel.DataAnnotations;    

    //namespace MyProject.Models.Validation
    //{

        /*public class ConsoleCheckAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                if (value.ToString() == "XboxOne" || value.ToString() == "PS4")
                {
                    return ValidationResult.Success;
                }


                return new ValidationResult("Please enter a valid console");
            }
        }*/
        
        //Makes sure both Console and the Game are valid
        public class ConsoleAndGameCheckAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                Object instance = validationContext.ObjectInstance;
                //Get the model
                Type type = instance.GetType();
                //Here is your property
                Object game = type.GetProperty("Game").GetValue(instance, null);
                string consoleString = value.ToString();
                string gameString = game.ToString();
                
                if (consoleString == "---")
                {
                    //If both are ---, all ok
                    if (gameString == "---")
                    {
                        return ValidationResult.Success;
                    }
                }else if (consoleString == "PS4")
                {
                    //If console is PS4 and games are God of war or crash bandicoot, all ok
                    if (gameString == "GoW" || gameString == "CB")  
                    {
                        return ValidationResult.Success;
                    }
                }else if (consoleString == "XboxOne")
                {
                    //If console is Xbox and games are Halo or gears of war, all ok
                    if (gameString == "Halo" || gameString == "GearoW")
                    {
                        return ValidationResult.Success;
                    }
                }
            return new ValidationResult("Please choose a valid console/game");
            }
        }

    public class AgreedToTermsCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            //Get the model
            Type type = instance.GetType();
            //Here is your property
            Object didAgree = type.GetProperty("AgreedToTerms").GetValue(instance, null);

            bool didAgreeBool = Boolean.Parse(didAgree.ToString());
            if (didAgreeBool)
            {
                return ValidationResult.Success;
            }


            return new ValidationResult("Please agree to Terms and Conditions");
        }
    }

    public class EnteredProperAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            //Get the model
            Type type = instance.GetType();
            //Here is your property
            Object is18OrOlder = type.GetProperty("Check18").GetValue(instance, null);

            int age;
            

            //int age = Int32.Parse(value.ToString());

            bool is18OrOlderBool = Boolean.Parse(is18OrOlder.ToString());
            if (is18OrOlderBool)
            {
                return ValidationResult.Success;
            }
            else if(value != null && Int32.TryParse(value.ToString(), out age))
            {
                if (age >= 1 && age <= 17)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Please enter a proper age");
        }
    }


    public class PasswordOptionalConditionsMet : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            //Get the model
            Type type = instance.GetType();
            //Here is your property
            Object password = type.GetProperty("Password").GetValue(instance, null);

            string passwordString = password.ToString();


            int counter = 0;
            if (passwordString.Any(char.IsUpper))
            {
                counter++;
            }

            if (passwordString.Any(char.IsLower))
            {
                counter++;
            }

            if (passwordString.Any(char.IsDigit))
            {
                counter++;
            }

            if(passwordString.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                counter++;
            }

            if (counter >= 3)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Not all conditions have been met");
            }
        }
    }
}