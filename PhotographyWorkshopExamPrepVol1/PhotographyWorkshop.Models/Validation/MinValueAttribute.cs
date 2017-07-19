﻿namespace PhotographyWorkshop.Models.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MinValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                int minValue = int.Parse(value.ToString());
                if (minValue < 100)
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch(OverflowException)
            {
                return false;
            }
        }
    }
}
