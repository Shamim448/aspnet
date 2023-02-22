using System.Text.RegularExpressions;
using XmlData;
using XMLFormetter;

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
                <Course>
                	<Title>Asp.net C#</Title>
                	<Teacher>
                		<Name>Md. Jalal Uddin</Name>
                		<Email>jalaluddin@devskill.com</Email>
                		<PresentAddress>
                			<Street>Mirpur-2</Street>
                			<City>Dhaka</City>
                			<Country>Bangladesh</Country>
                		</PresentAddress>
                		<PermanentAddress>
                			<Street>Moghbazar</Street>
                			<City>Dhaka</City>
                			<Country>Bangladesh</Country>
                		</PermanentAddress>
                		<PhoneNumbers>
                            <Phone>
                                <Number>828320328</Number>
                			    <Extension>382</Extension>
                			    <CountryCode>555</CountryCode>
                            </Phone>
                			<Phone>
                			    <Number>304303343</Number>
                			    <Extension>454</Extension>
                			    <CountryCode>343</CountryCode>
                		    </Phone>
                		</PhoneNumbers>
                	</Teacher>
                	<Topics>
                        <Topic>
                		    <Title>Gettig Started</Title>
                		    <Description>Frist Demo</Description>
                		    <Sessions>
                                <Session>
                			        <DurationInHour>2</DurationInHour>
                			        <LearningObjective>Start learning</LearningObjective>
                		        </Session>
                		        <Session>
                			        <DurationInHour>3</DurationInHour>
                			        <LearningObjective>Write Code</LearningObjective>
                		        </Session>
                		        <Session>
                			        <DurationInHour>4</DurationInHour>
                			        <LearningObjective>Run Code</LearningObjective>
                		        </Session>
                            </Sessions>
                        </Topic>
                        <Topic>
                		    <Title>Installation</Title>
                		    <Description>Tools</Description>
                            <Sessions>
                		        <Session>
                			        <DurationInHour>1</DurationInHour>
                			        <LearningObjective>VS Code</LearningObjective>
                		        </Session>
                		        <Session>
                			        <DurationInHour>4</DurationInHour>
                			        <LearningObjective>Docker</LearningObjective>
                		        </Session>
                		        <Session>
                			        <DurationInHour>2</DurationInHour>
                			        <LearningObjective>Git</LearningObjective>
                		        </Session>
                            </Sessions>
                	    </Topic>
                	    <Topic>
                		    <Title>Project</Title>
                		    <Description>Build Application</Description>
                		    <Sessions>
                                <Session>
                			        <DurationInHour>2</DurationInHour>
                			        <LearningObjective>Start learning</LearningObjective>
                                </Session>
                		        <Session>
                			        <DurationInHour>3</DurationInHour>
                			        <LearningObjective>Write Code</LearningObjective>
                		        </Session>
                		        <Session>
                			        <DurationInHour>4</DurationInHour>
                			        <LearningObjective>Run Code</LearningObjective>
                		        </Session>
                            </Sessions>
                	    </Topic>
                	</Topics>
                	<Fees>30000.5</Fees>
                	<Tests>
                        <Test>
                            <StartDateTime>2/3/2022 12:00:00 AM</StartDateTime>
                		    <EndDateTime>2/4/2022 12:00:00 AM</EndDateTime>
                		    <TestFees>100.5</TestFees>
                        </Test>
                		<Test>
                		    <StartDateTime>4/3/2023 12:00:00 AM</StartDateTime>
                		    <EndDateTime>4/4/2023 12:00:00 AM</EndDateTime>
                		    <TestFees>200.5</TestFees>
                	    </Test>
                	    <Test>
                		    <StartDateTime>5/3/2024 12:00:00 AM</StartDateTime>
                		    <EndDateTime>5/4/2024 12:00:00 AM</EndDateTime>
                		    <TestFees>300.5</TestFees>
                	    </Test>
                	</Tests>
                </Course>
                """;
            set2 = """
                <Product>
                	<Name>Camera</Name>
                	<BarCode>0475047503</BarCode>
                	<Description>A cannon camera</Description>
                    <Feedbacks>
                	    <Feedback>
                		    <FeedbackProivdername>Jalaluddin</FeedbackProivdername>
                		    <Rating>4.5</Rating>
                            <FeedbackItems>
                		        <FeedbackItem>
                			        <Name>Durability</Name>
                			        <Rating>3.2</Rating>
                		        </FeedbackItem>
                		        <FeedbackItem>
                			        <Name>User Friendliness</Name>
                			        <Rating>3.5</Rating>
                		        </FeedbackItem>
                            </FeedbackItems>
                	    </Feedback>
                	    <Feedback>
                		    <FeedbackProivdername>Tareq</FeedbackProivdername>
                		    <Rating>2.5</Rating>
                            <FeedbackItems>
                		        <FeedbackItem>
                			        <Name>Durability</Name>
                			        <Rating>2.2</Rating>
                		        </FeedbackItem>
                		        <FeedbackItem>
                			        <Name>User Friendliness</Name>
                			        <Rating>2.5</Rating>
                		        </FeedbackItem>
                            </FeedbackItems>
                	    </Feedback>
                    </Feedbacks>
                	<Spedificatios>
                        <Items>
                	        <SpedificationItem>
                		        <Name>Model</Name>
                		        <Value>Cannon</Value>
                	        </SpedificationItem>
                	        <SpedificationItem>
                		        <Name>Pixel</Name>
                		        <Value>12MPX</Value>
                	        </SpedificationItem>
                        </Items>
                	</Spedificatios>
                	<Price>30000.5</Price>
                    <Colors>
                	    <Color>
                		    <Name>FrontColor</Name>
                		    <Code>Black</Code>
                	    </Color>
                	    <Color>
                		    <Name>BackColor</Name>
                		    <Code>White</Code>
                	    </Color>
                    </Colors>
                </Product>
                """;
        }

        #endregion

        [Test]
        public void Test1()
        {
            set1 = new Regex(@"\s+").Replace(set1, "").Replace("\r\n", "");
            string xml = XmlFormatter.Convert(new XmlData.Course());
			xml = new Regex(@"\s+").Replace(xml, "").Replace("\r\n", "");

            Assert.AreEqual(0, string.Compare(set1, xml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void Test2()
        {
            set2 = new Regex(@"\s+").Replace(set2, "").Replace("\r\n", "");
            string xml = XmlFormatter.Convert(new Product());
			xml = new Regex(@"\s+").Replace(xml, "").Replace("\r\n", "");

            Assert.AreEqual(0, string.Compare(set2, xml, StringComparison.OrdinalIgnoreCase));
        }
    }
}