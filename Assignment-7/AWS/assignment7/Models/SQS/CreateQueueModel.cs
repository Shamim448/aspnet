using Amazon.SQS;
using Amazon.SQS.Model;

namespace assignment7.Models.SQS
{
    public class CreateQueueModel
    {
        public string QueueName { get; set; }
        public string Queueurl { get; set; }
        public async Task CreateQueueExample()
        {
            // Create service client using the SDK's default logic for determining AWS credentials and region to use.
            // For information configuring service clients checkout the .NET developer guide: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config.html
            AmazonSQSClient client = new AmazonSQSClient();

            var request = new CreateQueueRequest
            {
                QueueName = "Shamim44_AspnetB8",
                Attributes = new Dictionary<string, string>
                {
                    { "DelaySeconds", "60"},
                    { "MessageRetentionPeriod", "86400"}
                }
            };


            var response = await client.CreateQueueAsync(request);
            
            Console.WriteLine("The SQS queue's URL is {1}", response.QueueUrl);
        }

        public async Task<string> GetQueueUrlExample()
        {
            // Create service client using the SDK's default logic for determining AWS credentials and region to use.
            // For information configuring service clients checkout the .NET developer guide: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config.html
            AmazonSQSClient client = new AmazonSQSClient();

            var request = new GetQueueUrlRequest
            {
                QueueName = "Shamim44_AspnetB8",
            };

            GetQueueUrlResponse response = await client.GetQueueUrlAsync(request);
            Console.WriteLine("The SQS queue's URL is {0}", response.QueueUrl);
            Queueurl = response.QueueUrl;
            return Queueurl;
        }

        public async Task SendMssageExample()
        {
            // Create service client using the SDK's default logic for determining AWS credentials and region to use.
            // For information configuring service clients checkout the .NET developer guide: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config.html
            AmazonSQSClient client = new AmazonSQSClient();

            var sendMessageRequest = new SendMessageRequest
            {
                DelaySeconds = 10,
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    { "Title", new MessageAttributeValue { DataType = "String", StringValue = "The Whistler" } },
                    { "Author", new MessageAttributeValue { DataType = "String", StringValue = "John Grisham" } },
                    { "WeeksOn", new MessageAttributeValue { DataType = "Number", StringValue = "6" } }
                },
                MessageBody = "Information about current NY Times fiction bestseller for week of 12/11/2016.",
                QueueUrl = await GetQueueUrlExample()
            };

            var response = await client.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("Sent a message with id : {0}", response.MessageId);
        }


        public async Task ReceiveAndDeleteMessageExample()
        {
            // Create service client using the SDK's default logic for determining AWS credentials and region to use.
            // For information configuring service clients checkout the .NET developer guide: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config.html
            AmazonSQSClient client = new AmazonSQSClient();
            string queueUrl = await GetQueueUrlExample();

            //
            // Receive a single message
            //
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = queueUrl,
                VisibilityTimeout = 0,
                WaitTimeSeconds = 0
            };

            var receiveMessageResponse = await client.ReceiveMessageAsync(receiveMessageRequest);

            //
            // Delete the received single message
            //
            //var deleteMessageRequest = new DeleteMessageRequest
            //{
            //    QueueUrl = queueUrl,
            //    ReceiptHandle = receiveMessageResponse.Messages[0].ReceiptHandle
            //};

            //await client.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}
