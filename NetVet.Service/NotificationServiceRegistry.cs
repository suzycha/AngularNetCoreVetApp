using System;
using NetVet.Domain.Entities;
using NetVet.Domain.Repositories;

namespace NetVet.Service
{
    public class NotificationServiceRegistry : INotificationServiceRegistry
    {
        public INotificationService FindNotificationService(ContactTypes contactType)
        {
            // Return appropriate NotificationService based on contact type
            return contactType switch
            {
                ContactTypes.Mobile => new SmsNotificationService(),
                ContactTypes.EMail => new EmailNotificationService(),
                 _ => throw new ArgumentException($"Unsupported contact type: {contactType}") // updated version
                //_ => null // Return null if no service is supported
            };
        }
    }

    // Example implementation of SmsNotificationService
    public class SmsNotificationService : INotificationService
    {
        public ContactTypes ContactType => ContactTypes.Mobile;
        public void SendNotification(NotificationData notificationData)
        {
            // Logic to send SMS
            Console.WriteLine($"Sending SMS to {notificationData.ContactData}: " +
                              $"Reminder for {notificationData.PetName} at {notificationData.AppointmentDate}");
        }
    }

    // Example implementation of EmailNotificationService
    public class EmailNotificationService : INotificationService
    {
        public ContactTypes ContactType => ContactTypes.EMail;
        public void SendNotification(NotificationData notificationData)
        {
            // Logic to send Email
            Console.WriteLine($"Sending Email to {notificationData.ContactData}: " +
                              $"Reminder for {notificationData.PetName} at {notificationData.AppointmentDate}");
        }
    }
}