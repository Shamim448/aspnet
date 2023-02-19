using JSON_Serializer__Custom_;
using JsonData;
using System.Text.RegularExpressions;

namespace AssignmentTest
{
    public class Tests
    {
        private string set1;
        private string set2;

        #region Setup

        [SetUp]
        public void Setup()
        {
            set1 = """
                {
                	"Title": "Asp.net C#",
                	"Teacher": {
                		"Name": "Md. Jalal Uddin",
                		"Email": "jalaluddin@devskill.com",
                		"PresentAddress": {
                			"Street": "Mirpur-2",
                			"City": "Dhaka",
                			"Country": "Bangladesh"
                		},
                		"PermanentAddress": {
                			"Street": "Moghbazar",
                			"City": "Dhaka",
                			"Country": "Bangladesh"
                		},
                		"PhoneNumbers": [{
                			"Number": "828320328",
                			"Extension": "382",
                			"CountryCode": "555"
                		}, {
                			"Number": "304303343",
                			"Extension": "454",
                			"CountryCode": "343"
                		}]
                	},
                	"Topics": [{
                		"Title": "Gettig Started",
                		"Description": "Frist Demo",
                		"Sessions": [{
                			"DurationInHour": 2,
                			"LearningObjective": "Start learning"
                		}, {
                			"DurationInHour": 3,
                			"LearningObjective": "Write Code"
                		}, {
                			"DurationInHour": 4,
                			"LearningObjective": "Run Code"
                		}]
                	}, {
                		"Title": "Installation",
                		"Description": "Tools",
                		"Sessions": [{
                			"DurationInHour": 1,
                			"LearningObjective": "VS Code"
                		}, {
                			"DurationInHour": 4,
                			"LearningObjective": "Docker"
                		}, {
                			"DurationInHour": 2,
                			"LearningObjective": "Git"
                		}]
                	}, {
                		"Title": "Project",
                		"Description": "Build Application",
                		"Sessions": [{
                			"DurationInHour": 2,
                			"LearningObjective": "Start learning"
                		}, {
                			"DurationInHour": 3,
                			"LearningObjective": "Write Code"
                		}, {
                			"DurationInHour": 4,
                			"LearningObjective": "Run Code"
                		}]
                	}],
                	"Fees": 30000.5,
                	"Tests": [{
                		"StartDateTime": "2/3/2022 12:00:00 AM",
                		"EndDateTime": "2/4/2022 12:00:00 AM",
                		"TestFees": 100.5
                	}, {
                		"StartDateTime": "4/3/2023 12:00:00 AM",
                		"EndDateTime": "4/4/2023 12:00:00 AM",
                		"TestFees": 200.5
                	}, {
                		"StartDateTime": "5/3/2024 12:00:00 AM",
                		"EndDateTime": "5/4/2024 12:00:00 AM",
                		"TestFees": 300.5
                	}]
                }
                """;
            set2 = """
                {
                	"Name": "Camera",
                	"BarCode": "0475047503",
                	"Description": "A cannon camera",
                	"Feedbacks": [{
                		"FeedbackProivdername": "Jalaluddin",
                		"Rating": 4.5,
                		"FeedbackItems": [{
                			"Name": "Durability",
                			"Rating": 3.2
                		}, {
                			"Name": "User Friendliness",
                			"Rating": 3.5
                		}]
                	}, {
                		"FeedbackProivdername": "Tareq",
                		"Rating": 2.5,
                		"FeedbackItems": [{
                			"Name": "Durability",
                			"Rating": 2.2
                		}, {
                			"Name": "User Friendliness",
                			"Rating": 2.5
                		}]
                	}],
                	"Spedificatios": [{
                		"Items": [{
                			"Name": "Model",
                			"Value": "Cannon"
                		}, {
                			"Name": "Pixel",
                			"Value": "12MPX"
                		}]
                	}],
                	"Price": 30000.5,
                	"Colors": [{
                		"Name": "FrontColor",
                		"Code": "Black"
                	}, {
                		"Name": "BackColor",
                		"Code": "White"
                	}]
                }
                """;
        }

        #endregion

        [Test]
        public void Test1()
        {
            set1 = new Regex(@"\s+").Replace(set1, "").Replace("\r\n", "");
            string json = JsonFormatter.Convert(new JsonData.Course());
            json = new Regex(@"\s+").Replace(json, "").Replace("\r\n", "");

            Assert.AreEqual(0, string.Compare(set1, json, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void Test2()
        {
            set2 = new Regex(@"\s+").Replace(set2, "").Replace("\r\n", "");
            string json = JsonFormatter.Convert(new Product());
            json = new Regex(@"\s+").Replace(json, "").Replace("\r\n", "");

            Assert.AreEqual(0, string.Compare(set2, json, StringComparison.OrdinalIgnoreCase));
        }
    }
}