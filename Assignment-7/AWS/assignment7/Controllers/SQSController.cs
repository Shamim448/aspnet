using Amazon.SQS;
using Amazon.SQS.Model;
using assignment7.Models.SQS;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace assignment7.Controllers
{
    public class SQSController : Controller
    {
        private AmazonSQSClient sqsClient;
        private CancellationTokenSource? cts;
        public SQSController() {
            cts = new CancellationTokenSource();
            sqsClient = new AmazonSQSClient();
        }

        public async Task<IActionResult> Index()
        {
            List <ReceiveMessage> receiveMessages = new List<ReceiveMessage>();
            var queueUrlResponse = await sqsClient.GetQueueUrlAsync("Shamim44_AspnetB8");
            var receivedMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MaxNumberOfMessages = 10,
            };
            //while (!cts.IsCancellationRequested)
            //{
                ReceiveMessageResponse? response = await sqsClient.ReceiveMessageAsync(receivedMessageRequest/*, cts.Token*/);
            if(response.Messages.Count > 0) { 
                
            foreach (var message in response.Messages)
                {
                    receiveMessages.Add(new ReceiveMessage
                    {
                        MessageId = message.MessageId,
                        Message = message.Body,
                        Attribute = message.Attributes.Values.FirstOrDefault()

                    });
                }
            }
            await Task.Delay(1000);

                return View(receiveMessages);
            //}
            //return View(receiveMessages);
        }
        public async Task< IActionResult> CreateQueue()
        {
            var model = new CreateQueueModel();
             // await model.GetQueueUrlExample();
              await model.SendMssageExample();
            var a = 0;
            return View(model);
        }
        public async Task<IActionResult> SendMessage()
        {    
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(UserInfo model)
        {
            UserInfo userInfo = new UserInfo
            { 
                Email = model.Email,
                Name = model.Name,
                Id = Guid.NewGuid(),
            
            };
            var queueUrlResponse= await sqsClient.GetQueueUrlAsync("Shamim44_AspnetB8");
            var sendMessageRequese = new SendMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MessageBody = JsonSerializer.Serialize(userInfo),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {

                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = nameof(UserInfo)
                        }
                    }
                },
                DelaySeconds = 2,

                
            };
            var response = await sqsClient.SendMessageAsync(sendMessageRequese);
            return RedirectToAction("Index");
        }

    }
}
