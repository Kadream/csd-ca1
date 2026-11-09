using System;
using System.ComponentModel.DataAnnotations;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        // Category lower limits from the BP chart
        private const int HighSystolicMin = 140;
        private const int HighDiastolicMin = 90;
        private const int PreHighSystolicMin = 120;
        private const int PreHighDiastolicMin = 80;
        private const int IdealSystolicMin = 90;
        private const int IdealDiastolicMin = 60;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // Calculate BP category
        public BPCategory Category
        {
            get
            {
                if (Systolic >= HighSystolicMin || Diastolic >= HighDiastolicMin)
                {
                    return BPCategory.High;
                }

                if (Systolic >= PreHighSystolicMin || Diastolic >= PreHighDiastolicMin)
                {
                    return BPCategory.PreHigh;
                }

                if (Systolic >= IdealSystolicMin || Diastolic >= IdealDiastolicMin)
                {
                    return BPCategory.Ideal;
                }

                return BPCategory.Low;
            }
        }
    }
}
