using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Interfaces
{
    public class InterfacesDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Interfaces in Microsoft Teams-like Messaging System Demo ===\n");

            // Create a messaging system
            TeamsMessagingSystem messagingSystem = new("Contoso Corporation");

            // SECTION 1: CONTRACT-BASED PROGRAMMING
            Console.WriteLine("CONTRACT-BASED PROGRAMMING");
            Console.WriteLine("-------------------------");

            // Create users
            User user1 = new User("U1001", "John Smith", "john.smith@contoso.com");
            User user2 = new User("U1002", "Sarah Johnson", "sarah.johnson@contoso.com");
            User user3 = new User("U1003", "Michael Chen", "michael.chen@contoso.com");

            // Add users to the system
            messagingSystem.AddUser(user1);
            messagingSystem.AddUser(user2);
            messagingSystem.AddUser(user3);

            // Create different types of channels implementing the same interface
            IMessageChannel generalChannel = new TeamChannel("General", "Company-wide discussions");
            IMessageChannel marketingChannel = new TeamChannel("Marketing", "Marketing team discussions");
            IMessageChannel directMessageChannel = new DirectMessageChannel(user1, user2);

            // Add channels to the system
            messagingSystem.AddChannel(generalChannel);
            messagingSystem.AddChannel(marketingChannel);
            messagingSystem.AddChannel(directMessageChannel);

            // Demonstrate contract-based programming by sending messages through different channel types
            Console.WriteLine("\nSending messages through different channel types:");
            generalChannel.SendMessage(user1, "Hello everyone! Welcome to our Teams channel.");
            marketingChannel.SendMessage(user2, "We need to discuss the new product launch.");
            directMessageChannel.SendMessage(user1, "Hi Sarah, can we meet today?");

            // SECTION 2: INTERFACE SEGREGATION
            Console.WriteLine("\nINTERFACE SEGREGATION");
            Console.WriteLine("--------------------");

            // Create a meeting channel that implements multiple interfaces
            MeetingChannel productMeeting = new MeetingChannel("Product Review", "Weekly product review meeting");
            messagingSystem.AddChannel(productMeeting);

            // Use the channel through different interfaces
            Console.WriteLine("\nUsing a channel through different interfaces:");

            // As a message channel
            IMessageChannel messageChannel = productMeeting;
            messageChannel.SendMessage(user3, "I've prepared the slides for our meeting.");

            // As a meeting channel
            IMeetingCapable meetingChannel = productMeeting;
            Meeting meeting = meetingChannel.ScheduleMeeting(
                "Product Review - Sprint 23",
                DateTime.Now.AddDays(1).AddHours(10),
                TimeSpan.FromHours(1),
                user3
            );

            // As a file sharing channel
            IFileSharingCapable fileSharingChannel = productMeeting;
            fileSharingChannel.ShareFile(user3, "Q2_Product_Roadmap.pptx", 2048, "application/pptx");

            // SECTION 3: DEFAULT INTERFACE METHODS
            Console.WriteLine("\nDEFAULT INTERFACE METHODS");
            Console.WriteLine("-----------------------");

            // Demonstrate default interface methods
            Console.WriteLine("\nUsing default interface methods:");

            // Create a message with formatting
            IFormattableMessage formattedMessage = new TextMessage(user1, "This is *important* and _needs attention_");

            // Use the default formatting method
            string formattedText = formattedMessage.FormatMessage();
            Console.WriteLine($"Original: {formattedMessage.GetContent()}");
            Console.WriteLine($"Formatted: {formattedText}");

            // Create a custom message type that overrides the default formatting
            IFormattableMessage customMessage = new CodeMessage(user3, "function hello() { alert('Hello world!'); }");
            string customFormatted = customMessage.FormatMessage();
            Console.WriteLine($"Original code: {customMessage.GetContent()}");
            Console.WriteLine($"Formatted code: {customFormatted}");

            // SECTION 4: INTERFACE INHERITANCE
            Console.WriteLine("\nINTERFACE INHERITANCE");
            Console.WriteLine("-------------------");

            // Create a channel that implements an extended interface
            IAdvancedMessageChannel advancedChannel = new AdvancedTeamChannel("Engineering", "Engineering team discussions");
            messagingSystem.AddChannel(advancedChannel);

            Console.WriteLine("\nUsing interface inheritance:");

            // Use base interface methods
            advancedChannel.SendMessage(user1, "Has anyone reviewed my pull request?");

            // Use extended interface methods
            advancedChannel.SendPriorityMessage(user1, "Build is broken in the main branch!", MessagePriority.High);
            advancedChannel.EditMessage("MSG1005", "Build is broken in the development branch!");
            advancedChannel.PinMessage("MSG1005");

            // SECTION 5: EXPLICIT INTERFACE IMPLEMENTATION
            Console.WriteLine("\nEXPLICIT INTERFACE IMPLEMENTATION");
            Console.WriteLine("-------------------------------");

            // Create a notification service that explicitly implements multiple interfaces
            NotificationService notificationService = new NotificationService();

            Console.WriteLine("\nUsing explicit interface implementation:");

            // Use through IEmailNotifier interface
            IEmailNotifier emailNotifier = notificationService;
            emailNotifier.SendEmailNotification(user2, "New message in Marketing channel", "Sarah, there's a new message for you.");

            // Use through IPushNotifier interface
            IPushNotifier pushNotifier = notificationService;
            pushNotifier.SendPushNotification(user2, "New direct message", "John sent you a message.");

            // Use through IDesktopNotifier interface
            IDesktopNotifier desktopNotifier = notificationService;
            desktopNotifier.ShowDesktopAlert(user2, "Meeting reminder", "Product Review meeting starts in 15 minutes.");

            // SECTION 6: INTERFACE VS ABSTRACT CLASSES
            Console.WriteLine("\nINTERFACE VS ABSTRACT CLASSES");
            Console.WriteLine("--------------------------");

            Console.WriteLine("\nComparing interface and abstract class implementations:");

            // Create message processors using both approaches
            IMessageProcessor interfaceProcessor = new RegularMessageProcessor();
            MessageProcessorBase abstractProcessor = new PriorityMessageProcessor();

            // Process messages using both
            TextMessage message1 = new TextMessage(user1, "Regular message for processing");
            TextMessage message2 = new TextMessage(user2, "Priority message for processing");

            interfaceProcessor.ProcessMessage(message1);
            abstractProcessor.ProcessMessage(message2);

            // SECTION 7: DEPENDENCY INJECTION BASICS
            Console.WriteLine("\nDEPENDENCY INJECTION BASICS");
            Console.WriteLine("-------------------------");

            // Create message formatters
            IMessageFormatter plainFormatter = new PlainTextFormatter();
            IMessageFormatter htmlFormatter = new HtmlFormatter();
            IMessageFormatter markdownFormatter = new MarkdownFormatter();

            // Create message service with injected formatter
            Console.WriteLine("\nUsing dependency injection to switch formatters:");

            // With plain text formatter
            MessageService plainService = new MessageService(plainFormatter);
            plainService.FormatAndDisplayMessage("This is *bold* and _italic_ text");

            // With HTML formatter
            MessageService htmlService = new MessageService(htmlFormatter);
            htmlService.FormatAndDisplayMessage("This is *bold* and _italic_ text");

            // With Markdown formatter
            MessageService markdownService = new MessageService(markdownFormatter);
            markdownService.FormatAndDisplayMessage("This is *bold* and _italic_ text");

            // SECTION 8: INTERFACE-BASED PROGRAMMING
            Console.WriteLine("\nINTERFACE-BASED PROGRAMMING");
            Console.WriteLine("-------------------------");

            // Create a message archive system based on interfaces
            MessageArchiveSystem archiveSystem = new MessageArchiveSystem();

            // Create different types of messages
            IArchivableMessage textMsg = new TextMessage(user1, "This is a regular text message");
            IArchivableMessage codeMsg = new CodeMessage(user3, "var x = 10;");
            IArchivableMessage fileMsg = new FileMessage(user2, "report.xlsx", 1024, "application/excel");

            // Archive different message types through a common interface
            Console.WriteLine("\nArchiving different message types through a common interface:");
            archiveSystem.ArchiveMessage(textMsg);
            archiveSystem.ArchiveMessage(codeMsg);
            archiveSystem.ArchiveMessage(fileMsg);

            // Retrieve and display archived messages
            Console.WriteLine("\nRetrieving archived messages:");
            List<IArchivableMessage> archivedMessages = archiveSystem.GetArchivedMessages();
            foreach (var msg in archivedMessages)
            {
                Console.WriteLine($"- {msg.GetArchiveMetadata()} | Content: {msg.GetContent()}");
            }

            Console.WriteLine("\n=== End of Interfaces Demo ===");
        }
    }
}