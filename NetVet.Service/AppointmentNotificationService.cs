using System;
using System.Linq;
using System.Threading.Tasks;
using NetVet.Domain.Entities;
using NetVet.Domain.Repositories;

namespace NetVet.Service
{
    /// <summary>
    /// Sample implementation for sending notifications to owners with appointments
    /// tomorrow.
    /// </summary>
    /// <remarks>
    /// This sample is provided as an example of a service that might be running on a schedule
    /// as an example of the unit of work pattern (Mehdime) and repository pattern. This code is
    /// not complete / functional
    /// </remarks>
    public partial class AppointmentNotificationService : IAppoinmentNotificationService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INotificationServiceRegistry _notificationServiceRegistry;

        /// <summary>
        /// <see cref="IAppoinmentNotificationService.SendReminderForUpcomingAppointments"/>
        /// </summary>
        public AppointmentNotificationService(
            IAppointmentRepository appointmentRepository,
            INotificationServiceRegistry notificationServiceRegistry)
        {
            //_appointmentRepository = appointmentRepository;
            //_notificationServiceRegistry = notificationServiceRegistry;
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _notificationServiceRegistry = notificationServiceRegistry ?? throw new ArgumentNullException(nameof(notificationServiceRegistry));
        }

        /// <summary>
        /// Sends reminders for upcoming appointments (synchronous version).
        /// </summary>
        public void SendReminderForUpcomingAppointments()
        {
            // Call the async version and wait for completion.
            SendReminderForUpcomingAppointmentsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends reminders for upcoming appointments.
        /// </summary>
        public async Task SendReminderForUpcomingAppointmentsAsync()
        {
            DateTime targetStartDate = DateTime.Today.AddDays(1);
            DateTime targetEndDate = targetStartDate.AddDays(1);

            // Fetch
            var appointments = await _appointmentRepository.GetAppointmentsAsync(false);

            // Filter
            var ownersToSendReminders = appointments
                .Where(x => x.AppointmentDateTime >= targetStartDate
                            && x.AppointmentDateTime < targetEndDate
                            && x.Pet.Owner.IsOptInForNotifications)
                .Select(x => new
                {
                    Contacts = x.Pet.Owner.Contacts.Select(c => new { c.ContactType, c.ContactData }),
                    OwnerFirstName = x.Pet.Owner.FirstName,
                    OwnerPreferredName = x.Pet.Owner.PreferredName,
                    PetName = x.Pet.Name,
                    x.AppointmentDateTime
                }).ToList();

            // Send
            foreach (var owner in ownersToSendReminders)
            {
                string ownerName = owner.OwnerPreferredName ?? owner.OwnerFirstName;
                foreach (var contact in owner.Contacts)
                {
                    var notificationService = _notificationServiceRegistry.FindNotificationService(contact.ContactType);
                    if (notificationService == null)
                        continue;

                    var notificationData = new NotificationData(contact.ContactData, ownerName, owner.PetName, owner.AppointmentDateTime);
                    notificationService.SendNotification(notificationData);
                }
            }
        }
    }
}
