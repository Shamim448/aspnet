using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonData
{
    public class Product
    {
        protected Guid Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public Spedification[] Spedificatios { get; set; }
        public decimal Price { get; set; }
        public List<Color> Colors { get; set; }

        public Product()
        {
            Id = new Guid("931055D0-073C-4423-A010-18D3913E297C");
            Name = "Camera";
            Description = "A cannon camera";
            BarCode = "0475047503";

            Feedbacks = new List<Feedback>()
            {
                new Feedback()
                {
                    FeedbackProivdername = "Jalaluddin", Rating = 4.5,
                    FeedbackItems = new FeedbackItem[]
                    {
                        new FeedbackItem() { Rating =  3.2, Name = "Durability"},
                        new FeedbackItem() { Rating =  3.5, Name = "User Friendliness"},
                    }
                },
                new Feedback()
                {
                    FeedbackProivdername = "Tareq", Rating = 2.5,
                    FeedbackItems = new FeedbackItem[]
                    {
                        new FeedbackItem() { Rating = 2.2, Name = "Durability"},
                        new FeedbackItem() { Rating =  2.5, Name = "User Friendliness"},
                    }
                }
            };

            Spedificatios = new Spedification[]
            {
                new Spedification()
                {
                    Items = new List<SpedificationItem>
                    {
                        new SpedificationItem() { Name = "Model", Value = "Cannon"},
                        new SpedificationItem() { Name = "Pixel", Value = "12MPX"}
                    }
                }
            };

            Price = 30000.5m;

            Colors = new List<Color>()
            {
                new Color() { Name = "FrontColor", Code = "Black"},
                new Color() { Name = "BackColor", Code = "White"}
            };
        }
    }

    public class SpedificationItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Spedification
    {
        public List<SpedificationItem> Items { get; set; }
    }

    public class Feedback
    {
        public string FeedbackProivdername { get; set; }
        public double Rating { get; set; }
        public FeedbackItem[] FeedbackItems { get; set; }
    }

    public class FeedbackItem
    {
        public string Name { get; set; }
        public double Rating { get; set; }
    }

    public class Color
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
